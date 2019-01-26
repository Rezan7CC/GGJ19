using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField]
    private GameObject segment;

    private void Start()
    {
        Game.Instance.GameSignals.OnAstroidHitSegment += OnAstroidHit;
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
        if (hitSegment == segment.gameObject)
        {
            Debug.Log("boom");
            Game.Instance.GameModel.ReduceHealth(Game.Instance.GameSettings.astroidDamage);
        }
    }
}