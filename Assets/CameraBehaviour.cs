using DG.Tweening;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float ZoomSize = 4.5f;
    private float originalSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = Camera.main.orthographicSize;

        Game.Instance.GameModel.OnControlModeChanged += OnControlModeChange;
    }

    private void OnControlModeChange(ControlMode controlmode)
    {
        if (controlmode == ControlMode.ShieldMovement)
        {
            Camera.main.DOOrthoSize(ZoomSize, 0.25f);
        }
        else
        {
            Camera.main.DOOrthoSize(originalSize, 0.25f);
        }
    }
}
