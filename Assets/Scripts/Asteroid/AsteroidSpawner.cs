using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AsteroidConfig
{
    public float MinFrequency;
    public float MaxFrequency;

    public int MaximumLifeCount;

    public float MinSpeed;
    public float MaxSpeed;

    public float SpawnDistance;

    public float DistanceSpeedFactor = 1.0f;

    public bool TopLeftQuadrant;
    public bool TopRightQuadrant;
    public bool BottomLeftQuadrant;
    public bool BottomRightQuadrant;

    public float InnerTargetRadius;
    public float OuterTargetRadius;

    public float InnerTargetChance;

    public float DeadZone = 25.0f;

    public bool UrgentAlarm = false;
}

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidConfig[] asteroidStage = new AsteroidConfig[7];

    public float MinScale = 0.5f;
    public float MaxScale = 2.0f;

    private float spawnTimer = 0.0f;

    public GameObject AsteroidPrefab;
    public AsteroidDespawner AsteroidDespawnerInternal;
    public GameObject AstroidWarningIndicatorPrefab;
    public GameObject UICanvas;

    public AudioSource AlarmSource;
    public AudioClip NormalAlarmClip;
    public AudioClip UrgentAlarmClip;

    public int currentLifeCount = 0;
    public int currentStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeCount = 0;
        Game.Instance.GameSignals.OnHomeSegmentCountChanged += OnHomeSegmentsChanged;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime * Game.Instance.GameModel.InGameTimeScale;
        if (spawnTimer <= 0.0f)
        {
            spawnTimer = Random.Range(asteroidStage[currentStage].MinFrequency, asteroidStage[currentStage].MaxFrequency);
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        if (currentLifeCount >= asteroidStage[currentStage].MaximumLifeCount)
            return;

        if (!asteroidStage[currentStage].TopLeftQuadrant && !asteroidStage[currentStage].TopRightQuadrant 
            && !asteroidStage[currentStage].BottomLeftQuadrant && !asteroidStage[currentStage].BottomRightQuadrant)
            return;

        GameObject asteroidInstance = Instantiate<GameObject>(AsteroidPrefab);
        ++currentLifeCount;

        AsteroidMovement movement = asteroidInstance.GetComponent<AsteroidMovement>();
        movement.Speed = Random.Range(asteroidStage[currentStage].MinSpeed, asteroidStage[currentStage].MaxSpeed);

        float rotationAngle = 0.0f;
        for(int i = 0; i < 1000; ++i)
        {
            int spawnQuadrant = Random.Range(0, 4);
            switch(spawnQuadrant)
            {
                case 0:
                {
                        if (!asteroidStage[currentStage].TopLeftQuadrant)
                            continue;
                        rotationAngle = Random.Range(asteroidStage[currentStage].DeadZone, 90);
                    break;
                }
                case 1:
                    {
                        if (!asteroidStage[currentStage].TopRightQuadrant)
                            continue;
                        rotationAngle = Random.Range(240, 360 - asteroidStage[currentStage].DeadZone);
                        break;
                    }
                case 2:
                    {
                        if (!asteroidStage[currentStage].BottomLeftQuadrant)
                            continue;
                        rotationAngle = Random.Range(90, 180 - asteroidStage[currentStage].DeadZone);
                        break;
                    }
                case 3:
                    {
                        if (!asteroidStage[currentStage].BottomRightQuadrant)
                            continue;
                        rotationAngle = Random.Range(180 + asteroidStage[currentStage].DeadZone, 240);
                        break;
                    }
            }
        }


        Vector3 spawnDir = Quaternion.Euler(0, 0, rotationAngle) * new Vector3(0, 1, 0);
        float spawnDist = asteroidStage[currentStage].SpawnDistance;
        asteroidInstance.transform.position = spawnDir *
            Mathf.LerpUnclamped(spawnDist, spawnDist * movement.Speed, asteroidStage[currentStage].DistanceSpeedFactor);

        float scale = Random.Range(MinScale, MaxScale);
        asteroidInstance.transform.localScale = new Vector3(scale, scale, scale);

        float circleResult = Random.Range(0.0f, 1.0f);

        Vector3 targetPosition;
        if(asteroidStage[currentStage].InnerTargetChance >= circleResult)
        {
            targetPosition = Random.insideUnitCircle * asteroidStage[currentStage].InnerTargetRadius;
        }
        else
        {
            targetPosition = Random.insideUnitCircle * asteroidStage[currentStage].OuterTargetRadius;
        }

        movement.Direction = (targetPosition - asteroidInstance.transform.position).normalized;
        movement.AsteroidDespawnerInternal = AsteroidDespawnerInternal;
        movement.AsteroidSpawnerInternal = this;

        GameObject warningIndicator = Instantiate<GameObject>(AstroidWarningIndicatorPrefab);
        AlarmSource.PlayOneShot(asteroidStage[currentStage].UrgentAlarm ? UrgentAlarmClip : NormalAlarmClip);
        warningIndicator.transform.SetParent(UICanvas.transform);
        movement.AstroidWarningIndicator = warningIndicator.GetComponent<RectTransform>();
        movement.Init();
    }
    
    private void OnHomeSegmentsChanged(int value)
    {
        currentStage = value;
    }
}

