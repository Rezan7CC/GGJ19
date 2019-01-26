using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float MinFrequency = 2.0f;
    public float MaxFrequency = 5.0f;

    public int MaximumLifeCount = 5;

    public float MinSpeed = 0.5f;
    public float MaxSpeed = 2.0f;

    public GameObject AsteroidPrefab;
    public BoxCollider AsteroidDespawner;

    public float SpawnDistance = 10.0f;

    public bool TopLeftQuadrant = true;
    public bool TopRightQuadrant = true;
    public bool BottomLeftQuadrant = true;
    public bool BottomRightQuadrant = false;

    public float InnerTargetRadius = 3.0f;
    public float OuterTargetRadius = 7.0f;

    public float InnerTargetChance = 0.5f;

    private float spawnTimer = 0.0f;

    public int currentLifeCount = 0;

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
            spawnTimer = Random.Range(MinFrequency, MaxFrequency);
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        if (currentLifeCount >= MaximumLifeCount)
            return;

        if (!TopLeftQuadrant && !TopRightQuadrant && !BottomLeftQuadrant && !BottomRightQuadrant)
            return;

        GameObject asteroidInstance = Instantiate<GameObject>(AsteroidPrefab);
        ++currentLifeCount;

        AsteroidMovement movement = asteroidInstance.GetComponent<AsteroidMovement>();
        movement.Speed = Random.Range(MinSpeed, MaxSpeed);

        float rotationAngle = 0.0f;
        for(int i = 0; i < 1000; ++i)
        {
            int spawnQuadrant = Random.Range(0, 4);
            switch(spawnQuadrant)
            {
                case 0:
                {
                        if (!TopLeftQuadrant)
                            continue;
                        rotationAngle = Random.Range(0, 90);
                    break;
                }
                case 1:
                    {
                        if (!TopRightQuadrant)
                            continue;
                        rotationAngle = Random.Range(240, 360);
                        break;
                    }
                case 2:
                    {
                        if (!BottomLeftQuadrant)
                            continue;
                        rotationAngle = Random.Range(90, 180);
                        break;
                    }
                case 3:
                    {
                        if (!BottomRightQuadrant)
                            continue;
                        rotationAngle = Random.Range(180, 240);
                        break;
                    }
            }
        }


        Vector3 spawnDir = Quaternion.Euler(0, 0, rotationAngle) * new Vector3(0, 1, 0);
        asteroidInstance.transform.position = spawnDir * SpawnDistance;

        float circleResult = Random.Range(0.0f, 1.0f);

        Vector3 targetPosition;
        if(InnerTargetChance >= circleResult)
        {
            targetPosition = Random.insideUnitCircle * InnerTargetRadius;
        }
        else
        {
            targetPosition = Random.insideUnitCircle * OuterTargetRadius;
        }

        movement.Direction = (targetPosition - asteroidInstance.transform.position).normalized;
        movement.AsteroidDespawner = AsteroidDespawner;
        movement.AsteroidSpawnerInternal = this;
    }
}

