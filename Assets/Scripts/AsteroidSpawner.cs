using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float MinFrequency = 2.0f;
    public float MaxFrequency = 5.0f;

    public float MinSpeed = 0.5f;
    public float MaxSpeed = 2.0f;

    public GameObject AsteroidPrefab;

    public float SpawnDistance = 10.0f;

    private float spawnTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

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
        GameObject asteroidInstance = Instantiate<GameObject>(AsteroidPrefab);

        AsteroidMovement movement = asteroidInstance.GetComponent<AsteroidMovement>();
        movement.Speed = Random.Range(MinSpeed, MaxSpeed);

        Vector3 spawnDir = Quaternion.Euler(0, 0, Random.Range(0, 360.0f)) * new Vector3(0, 1, 0);
        asteroidInstance.transform.position = spawnDir * SpawnDistance;

        movement.Direction = -spawnDir;
    }
}

