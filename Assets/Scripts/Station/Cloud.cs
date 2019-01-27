using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject CloudObject;
    public Transform Center;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CloudObject.transform.RotateAround(Center.position, new Vector3(0, 0, 1), Speed);
    }
}
