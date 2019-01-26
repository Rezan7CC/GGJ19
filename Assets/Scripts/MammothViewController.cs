using UnityEngine;

public class MammothViewController : MonoBehaviour
{
    public Vector3 CollectPosition;
    public Vector3 CollectRotation;

    public void DockToResourcePlanet()
    {
        transform.position = CollectPosition;
        transform.eulerAngles = CollectRotation;
    }
}
