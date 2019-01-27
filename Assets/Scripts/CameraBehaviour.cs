using DG.Tweening;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float ZoomSize = 4.5f;
    public float XLandingOffset = 0.2f;
    private float originalSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = Camera.main.orthographicSize;

        Game.Instance.GameModel.OnControlModeChanged += OnControlModeChange;
    }

    private void OnControlModeChange(ControlMode controlmode, ControlMode previousMode)
    {
        if (controlmode == ControlMode.ShieldMovement)
        {
            Camera.main.DOOrthoSize(ZoomSize, 0.25f);
        }
        else if (controlmode == ControlMode.ShipMovement)
        {
            DOTween.Sequence()
                .Insert(0, Camera.main.DOOrthoSize(originalSize, 0.25f))
                .Insert(0, transform.DOMoveX(0f, 0.25f));
            ;
        }
        else if (controlmode == ControlMode.ResourceGathering)
        {
            DOTween.Sequence()
                .Insert(0, Camera.main.DOOrthoSize(ZoomSize, 0.25f))
                .Insert(0, transform.DOMoveX(XLandingOffset, 0.25f));
        }
    }
}
