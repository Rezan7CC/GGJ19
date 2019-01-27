using DG.Tweening;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField]
    private GameObject segment;

    private bool _isInvincible = false;

    private void Start()
    {
        Game.Instance.GameSignals.OnAstroidHitSegment += OnAstroidHit;
        Game.Instance.GameSignals.OnWin += OnWin;
    }

    private void OnWin()
    {
        _isInvincible = true;
    }

    public void HideSegment()
    {
        if (IsShown())
        {
            segment.transform.localScale = Vector3.zero;
        }
    }
    
    public void ShowSegment()
    {
        if (IsShown() == false)
        {
            segment.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        }
    }

    private bool IsShown()
    {
        return segment.transform.localScale != Vector3.zero;
    }
    
    private void OnAstroidHit(GameObject hitSegment)
    {
        if (hitSegment == segment.gameObject && !_isInvincible)
        {
            Game.Instance.GameModel.ReduceHealth(Game.Instance.GameSettings.astroidDamage);
        }
    }
}