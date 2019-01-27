using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class DynamicButtonIndicator : MonoBehaviour, IResetable
{
    public GameObject HomeBaseSnapIndicator;
    public GameObject ResourcePlanetSnapIndicator;
    
    public GameObject HomeBaseControlsIndicator;
    public GameObject ResourcePlanetControlsIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.GameSignals.OnTriggerCollisionEnter += OnTriggerCollisionEnter;
        Game.Instance.GameSignals.OnTriggerCollisionExit += OnTriggerCollisionExit;
        Game.Instance.GameModel.OnControlModeChanged += OnControlModeChanged;
    }

    private void OnControlModeChanged(ControlMode controlmode, ControlMode previousMode)
    {
        if (controlmode == ControlMode.ResourceGathering)
        {
            Hide(ResourcePlanetSnapIndicator);
            Show(ResourcePlanetControlsIndicator);
        }
        else if (controlmode == ControlMode.ShieldMovement)
        {
            Hide(HomeBaseSnapIndicator);
            Show(HomeBaseControlsIndicator);
        }
        else if (controlmode == ControlMode.ShipMovement)
        {
            Hide(HomeBaseControlsIndicator);
            Hide(ResourcePlanetControlsIndicator);

            if (previousMode == ControlMode.ResourceGathering)
            {
                Show(ResourcePlanetSnapIndicator);
            }
            else if (previousMode == ControlMode.ShieldMovement)
            {
                Show(HomeBaseSnapIndicator);
            }
        }
    }

    private void OnTriggerCollisionExit(TriggerCollisionType type)
    {
        if (type == TriggerCollisionType.HomeStation)
        {
            Hide(HomeBaseSnapIndicator);
        }
        else if (type == TriggerCollisionType.ResourcePlanet)
        {
            Hide(ResourcePlanetSnapIndicator);
        }
    }

    private void OnTriggerCollisionEnter(TriggerCollisionType type)
    {
        if (type == TriggerCollisionType.HomeStation)
        {
            Show(HomeBaseSnapIndicator);
        }
        else if (type == TriggerCollisionType.ResourcePlanet)
        {
            Show(ResourcePlanetSnapIndicator);
        }
    }

    private void Show(GameObject indicatorObject)
    {
        indicatorObject.transform.DOScale(1, 0.15f).SetEase(Ease.OutBack);
    }

    private void Hide(GameObject indicatorObject, float duration = 0.15f)
    {
        if (indicatorObject.transform.localScale != Vector3.zero)
        {
            indicatorObject.transform.DOScale(0, duration);
        }
    }

    public void Reset()
    {
        Hide(HomeBaseSnapIndicator, 0);
        Hide(ResourcePlanetSnapIndicator, 0);
        Hide(HomeBaseControlsIndicator, 0);
        Hide(ResourcePlanetControlsIndicator, 0);
    }
}
