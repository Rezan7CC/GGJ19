using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidMovement : MonoBehaviour, IResetable
{
    public float MinRotationSpeed = 0.0f;
    public float MaxRotationSpeed = 5.0f;
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
        GetComponent<Rigidbody>().AddTorque(Random.Range(MinRotationSpeed, MaxRotationSpeed),
           Random.Range(MinRotationSpeed, MaxRotationSpeed), Random.Range(MinRotationSpeed, MaxRotationSpeed), ForceMode.VelocityChange);
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

    void DestroyAsteroid(float positionShake = 0, float rotationShake = 0)
    {
        --AsteroidSpawnerInternal.currentLifeCount;
        Destroy(gameObject);
        if ((positionShake > 0 || rotationShake > 0) && Camera.main != null)
        {
            DOTween.Sequence()
                .Insert(0, Camera.main.transform.DOShakePosition(0.33f, positionShake))
                .Insert(0, Camera.main.transform.DOShakeRotation(0.33f, rotationShake));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals(Tags.Shield))
        {
            DestroyAsteroid(0.03f, 0.17f);
        }

        if (other.gameObject.tag.Equals(Tags.Segment))
        {
            DestroyAsteroid(0.05f, 0.33f);
            
            if (Game.Instance.GameSignals.OnAstroidHitSegment != null)
            {
                Game.Instance.GameSignals.OnAstroidHitSegment(other.gameObject);
            }
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
            //AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Mathf.Abs(angleDeg) > 135)
        {
            AstroidWarningIndicator.position = new Vector3(ClampPixelWidth(positionScreenSpace.x), offset, 0);
            //AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (angleDeg > 0)
        {
            AstroidWarningIndicator.position = new Vector3(mainCamera.scaledPixelWidth - offset, ClampPixelHeight(positionScreenSpace.y), 0);
            //AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            AstroidWarningIndicator.position = new Vector3(offset, ClampPixelHeight(positionScreenSpace.y), 0);
            //AstroidWarningIndicator.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void Reset()
    {
        DestroyAsteroid();
    }
}
