using DefaultNamespace;
using UnityEngine;

public class Shield : MonoBehaviour, IResetable
{
    public GameObject ShieldObject;
    public Transform Center;
    public float Speed;

    private Vector3 originPosition;
    private Quaternion originRotation;

    private void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    // Update is called once per frame
    public void HandleMovement()
    {
        ShieldObject.transform.RotateAround(Center.position, new Vector3(0, 0, 1), Input.GetAxis("Horizontal") * -Speed);
    }

    public void Reset()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
    }
}
