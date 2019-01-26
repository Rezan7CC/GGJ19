using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [HideInInspector]
    public float Speed = 0.0f;

    [HideInInspector]
    public Vector3 Direction;

    [HideInInspector]
    public BoxCollider AsteroidDespawner;

    [HideInInspector]
    public AsteroidSpawner AsteroidSpawnerInternal;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;

        if(transform.position.x > AsteroidDespawner.transform.position.x + AsteroidDespawner.size.x * 0.5)
        {
            DestroyAsteroid();
        }
        if (transform.position.x < AsteroidDespawner.transform.position.x - AsteroidDespawner.size.x * 0.5)
        {
            DestroyAsteroid();
        }
        if (transform.position.y > AsteroidDespawner.transform.position.y + AsteroidDespawner.size.y * 0.5)
        {
            DestroyAsteroid();
        }
        if (transform.position.y < AsteroidDespawner.transform.position.y - AsteroidDespawner.size.y * 0.5)
        {
            DestroyAsteroid();
        }
    }

    void DestroyAsteroid()
    {
        --AsteroidSpawnerInternal.currentLifeCount;
        Destroy(gameObject);
    }
}
