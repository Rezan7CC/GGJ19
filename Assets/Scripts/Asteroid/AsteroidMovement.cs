using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidMovement : MonoBehaviour
{
    public float OffsetBufferPixel = 10.0f;

    [HideInInspector]
    public float Speed = 0.0f;

    [HideInInspector]
    public Vector3 Direction;

    [HideInInspector]
    public AsteroidDespawner AsteroidDespawnerInternal;

    [HideInInspector]
    public AsteroidSpawner AsteroidSpawnerInternal;

    [HideInInspector]
    public RectTransform AstroidWarningIndicator;

    private BoxCollider asteroidDespawnerBox;
    private Transform asteroidDespawnerTransform;
    private bool wasInView = false;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init()
    {
        asteroidDespawnerBox = AsteroidDespawnerInternal.boxCollider;
        asteroidDespawnerTransform = AsteroidDespawnerInternal.transform;
        UpdateIndicatorTransform(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
         transform.position += Direction * Speed * Time.deltaTime;

        if(!wasInView && transform.position.x < asteroidDespawnerTransform.position.x + asteroidDespawnerBox.size.x * 0.5
            && transform.position.x > asteroidDespawnerTransform.position.x - asteroidDespawnerBox.size.x * 0.5
            && transform.position.y < asteroidDespawnerTransform.position.y + asteroidDespawnerBox.size.y * 0.5
            && transform.position.y > asteroidDespawnerTransform.position.y - asteroidDespawnerBox.size.y * 0.5)
        {
            Destroy(AstroidWarningIndicator.gameObject);
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
        else
        {
            UpdateIndicatorTransform(transform.position);
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

    float ClampPixelHeight(float height)
    {
        float offset = AstroidWarningIndicator.rect.height * 0.5f + OffsetBufferPixel;
        return Mathf.Clamp(height, offset, mainCamera.scaledPixelHeight - offset);
    }

    float ClampPixelWidth(float width)
    {
        float offset = AstroidWarningIndicator.rect.height * 0.5f + OffsetBufferPixel;
        return Mathf.Clamp(width, offset, mainCamera.scaledPixelWidth - offset);
    }

    void UpdateIndicatorTransform(Vector3 asteroidPosition)
    {
        Vector3 positionScreenSpace = mainCamera.WorldToScreenPoint(asteroidPosition);
        Vector3 direction = asteroidPosition.normalized;

        float angleDeg = Mathf.Acos(Vector3.Dot(direction, Vector3.up)) * Mathf.Rad2Deg;
        if (transform.position.x < 0)
            angleDeg = -angleDeg;

        float offset = AstroidWarningIndicator.rect.height * 0.5f + OffsetBufferPixel;

        if (Mathf.Abs(angleDeg) < 45)
        {
            AstroidWarningIndicator.position = new Vector3(ClampPixelWidth(positionScreenSpace.x), mainCamera.scaledPixelHeight - offset, 0);
            AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Mathf.Abs(angleDeg) > 135)
        {
            AstroidWarningIndicator.position = new Vector3(ClampPixelWidth(positionScreenSpace.x), offset, 0);
            AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (angleDeg > 0)
        {
            AstroidWarningIndicator.position = new Vector3(mainCamera.scaledPixelWidth - offset, ClampPixelHeight(positionScreenSpace.y), 0);
            AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            AstroidWarningIndicator.position = new Vector3(offset, ClampPixelHeight(positionScreenSpace.y), 0);
            AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
