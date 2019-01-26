using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField]
    private GameObject segment;

    private bool _canTakeDamage = false;

    private void Start()
    {
        Game.Instance.GameSignals.OnAstroidHitSegment += OnAstroidHit;
        Game.Instance.GameSignals.OnWin += OnWin;
    }

    private void OnWin()
    {
        _canTakeDamage = true;
    }

    public void HideSegment()
    {
        if (IsShown())
        {
            segment.SetActive(false);
        }
    }
    
    public void ShowSegment()
    {
        if (IsShown() == false)
        {
            segment.SetActive(true);
        }
    }

    private bool IsShown()
    {
        return segment.activeSelf;
    }
    
    private void OnAstroidHit(GameObject hitSegment)
    {
        if (hitSegment == segment.gameObject && !_canTakeDamage)
        {
            Game.Instance.GameModel.ReduceHealth(Game.Instance.GameSettings.astroidDamage);
        }
    }
}