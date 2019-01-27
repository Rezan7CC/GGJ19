using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditStars : MonoBehaviour
{
    public float ScrollSpeed = 1000.0f;
    public RectTransform stars1Transform;
    public RectTransform stars2Transform;

    private float startHeight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = stars1Transform.position.y;
        stars1Transform.position = new Vector3(Camera.main.pixelWidth * 0.5f, startHeight);
        stars2Transform.position = new Vector3(Camera.main.pixelWidth + Camera.main.pixelWidth * 0.5f, startHeight);
    }

    // Update is called once per frame
    void Update()
    {
        stars1Transform.position -= new Vector3(ScrollSpeed * Time.deltaTime, 0.0f);
        if (stars1Transform.position.x < -Camera.main.pixelWidth + Camera.main.pixelWidth * 0.5f)
            stars1Transform.position = new Vector3(Camera.main.pixelWidth + Camera.main.pixelWidth * 0.5f, startHeight);

        stars2Transform.position -= new Vector3(ScrollSpeed * Time.deltaTime, 0.0f);
        if (stars2Transform.position.x < -Camera.main.pixelWidth + Camera.main.pixelWidth * 0.5f)
            stars2Transform.position = new Vector3(Camera.main.pixelWidth + Camera.main.pixelWidth * 0.5f, startHeight);
    }

    float CameraWidth()
    {
        return Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }
}
