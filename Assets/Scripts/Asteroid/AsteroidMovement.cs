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
    public AsteroidDespawner AsteroidDespawnerInternal;

    [HideInInspector]
    public AsteroidSpawner AsteroidSpawnerInternal;

    private BoxCollider asteroidDespawnerBox;
    private Transform asteroidDespawnerTransform;
    private bool wasInView = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init()
    {
        asteroidDespawnerBox = AsteroidDespawnerInternal.boxCollider;
        asteroidDespawnerTransform = AsteroidDespawnerInternal.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;

        if(transform.position.x < asteroidDespawnerTransform.position.x + asteroidDespawnerBox.size.x * 0.5
            && transform.position.x > asteroidDespawnerTransform.position.x - asteroidDespawnerBox.size.x * 0.5
            && transform.position.y < asteroidDespawnerTransform.position.y + asteroidDespawnerBox.size.y * 0.5
            && transform.position.y > asteroidDespawnerTransform.position.y - asteroidDespawnerBox.size.y * 0.5)
        {
            wasInView = true;
        }

        if(wasInView)
        {
            if (transform.position.x > asteroidDespawnerTransform.position.x + asteroidDespawnerBox.size.x * 0.5)
            {
                DestroyAsteroid();
            }
            if (transform.position.x < asteroidDespawnerTransform.position.x - asteroidDespawnerBox.size.x * 0.5)
            {
                DestroyAsteroid();
            }
            if (transform.position.y > asteroidDespawnerTransform.position.y + asteroidDespawnerBox.size.y * 0.5)
            {
                DestroyAsteroid();
            }
            if (transform.position.y < asteroidDespawnerTransform.position.y - asteroidDespawnerBox.size.y * 0.5)
            {
                DestroyAsteroid();
            }
        }
    }

    void DestroyAsteroid()
    {
        --AsteroidSpawnerInternal.currentLifeCount;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals(Tags.Shield))
        {
            DestroyAsteroid();
        }
    }
}
