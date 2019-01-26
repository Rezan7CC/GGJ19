using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject ShieldObject;
    public Transform Center;
    public float Speed;
    
    // Update is called once per frame
    public void HandleMovement()
    {
        ShieldObject.transform.RotateAround(Center.position, new Vector3(0, 0, 1), Input.GetAxis("Horizontal") * -Speed);
    }
}
