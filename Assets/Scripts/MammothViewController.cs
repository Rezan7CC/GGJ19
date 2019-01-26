using DefaultNamespace;
using UnityEngine;

public class MammothViewController : MonoBehaviour, IResetable
{
    public Vector3 CollectPosition;
    public Vector3 CollectRotation;

    private Vector3 originPosition;
    private Quaternion originRotation;

    private void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    public void DockToResourcePlanet()
    {
        transform.position = CollectPosition;
        transform.eulerAngles = CollectRotation;
    }

    public void Reset()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
    }

    public void DockToHomebase()
    {
        transform.position = Vector3.zero;
    }
}
