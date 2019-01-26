using DefaultNamespace;
using UnityEngine;

public class HomeBaseSnapIndicator : MonoBehaviour, IResetable
{
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.GameSignals.OnTriggerCollisionEnter += OnTriggerCollisionEnter;
        Game.Instance.GameSignals.OnTriggerCollisionExit += OnTriggerCollisionExit;
    }

    private void OnTriggerCollisionExit(TriggerCollisionType type)
    {
        if (type == TriggerCollisionType.HomeStation)
        {
            Hide();
        }
    }

    private void OnTriggerCollisionEnter(TriggerCollisionType type)
    {
        if (type == TriggerCollisionType.HomeStation)
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        Hide();
    }
}
