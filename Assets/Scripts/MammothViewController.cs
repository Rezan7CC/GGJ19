using DefaultNamespace;
using DG.Tweening;
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

        Game.Instance.GameSignals.OnWin += OnWin;
    }

    private void OnWin()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
    }

    public void DockToResourcePlanet()
    {
        DOTween.Sequence()
            .Insert(0, transform.DOMove(CollectPosition, 0.2f))
            .Insert(0, transform.DORotate(CollectRotation, 0.2f));

    }

    public void Reset()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
    }

    public void DockToHomebase(Vector3 transformRotation)
    {
        DOTween.Sequence()
            .Insert(0, transform.DOMove(Vector3.zero, 0.2f))
            .Insert(0, transform.DORotate(transformRotation, 0.2f));
    }
}
