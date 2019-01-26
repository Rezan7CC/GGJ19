using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDespawner : MonoBehaviour
{
    public Vector2 buffer = new Vector2(2, 2);

    public Camera _camera;
    public BoxCollider boxCollider;

    [HideInInspector]
    public Vector2 cameraBounds;

    // Start is called before the first frame update
    void Start()
    {
        cameraBounds = new Vector2(_camera.orthographicSize * 2 * _camera.aspect, _camera.orthographicSize * 2);
        boxCollider.size = new Vector3(cameraBounds.x + buffer.x, cameraBounds.y + buffer.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
