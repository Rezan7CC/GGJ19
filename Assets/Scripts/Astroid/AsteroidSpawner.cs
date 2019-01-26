using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidConfig
{
    AsteroidConfig(float minFrequency, float maxFrequency, int maximumLifeCount, float minSpeed, float maxSpeed, float spawnDistance, 
        bool topLeftQuadrant, bool topRightQuadrant, bool bottomLeftQuadrant, bool bottomRightQuadrant, 
        float innerTargetRadius, float outerTargetRadius, float innerTargetChance)
    {
        MinFrequency = minFrequency;
        MaxFrequency = maxFrequency;
        MaximumLifeCount = maximumLifeCount;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
        SpawnDistance = spawnDistance;
        TopLeftQuadrant = topLeftQuadrant;
        TopRightQuadrant = topRightQuadrant;
        BottomLeftQuadrant = bottomLeftQuadrant;
        BottomRightQuadrant = bottomRightQuadrant;
        InnerTargetRadius = innerTargetRadius;
        OuterTargetRadius = outerTargetRadius;
        InnerTargetChance = innerTargetChance;
    }

    public float MinFrequency;
    public float MaxFrequency;

    public int MaximumLifeCount;

    public float MinSpeed;
    public float MaxSpeed;

    public float SpawnDistance;

    public bool TopLeftQuadrant;
    public bool TopRightQuadrant;
    public bool BottomLeftQuadrant;
    public bool BottomRightQuadrant;

    public float InnerTargetRadius;
    public float OuterTargetRadius;

    public float InnerTargetChance;
}

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidConfig[] asteroidStage = new AsteroidConfig[6];

    private float spawnTimer = 0.0f;

    public GameObject AsteroidPrefab;
    public BoxCollider AsteroidDespawner;

    public int currentLifeCount = 0;
    public int currentStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
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
                        rotationAngle = Random.Range(0, 90);
                    break;
                }
                case 1:
                    {
                        if (!asteroidStage[currentStage].TopRightQuadrant)
                            continue;
                        rotationAngle = Random.Range(240, 360);
                        break;
                    }
                case 2:
                    {
                        if (!asteroidStage[currentStage].BottomLeftQuadrant)
                            continue;
                        rotationAngle = Random.Range(90, 180);
                        break;
                    }
                case 3:
                    {
                        if (!asteroidStage[currentStage].BottomRightQuadrant)
                            continue;
                        rotationAngle = Random.Range(180, 240);
                        break;
                    }
            }
        }


        Vector3 spawnDir = Quaternion.Euler(0, 0, rotationAngle) * new Vector3(0, 1, 0);
        asteroidInstance.transform.position = spawnDir * asteroidStage[currentStage].SpawnDistance;

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
        movement.AsteroidDespawner = AsteroidDespawner;
        movement.AsteroidSpawnerInternal = this;
    }
}

