using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class MammothViewController : MonoBehaviour, IResetable
{
    public Transform collectTransform;
    public Transform startTransform;

    private void Start()
    {
        transform.SetPositionAndRotation(startTransform.position, startTransform.rotation);

        Game.Instance.GameSignals.OnWin += OnWin;
    }

    private void OnWin()
    {
        transform.SetPositionAndRotation(collectTransform.position, collectTransform.rotation);
    }

    public void DockToResourcePlanet()
    {
        DOTween.Sequence()
            .Insert(0, transform.DOMove(collectTransform.position, 0.2f))
            .Insert(0, transform.DORotate(collectTransform.rotation.eulerAngles, 0.2f));

    }

    public void Reset()
    {
        transform.SetPositionAndRotation(startTransform.position, startTransform.rotation);
    }

    public void DockToHomebase(Vector3 transformRotation)
    {
        DOTween.Sequence()
            .Insert(0, transform.DOMove(Vector3.zero, 0.2f))
            .Insert(0, transform.DORotate(transformRotation, 0.2f));
    }
}
