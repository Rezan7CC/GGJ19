using DG.Tweening;
using UnityEngine;

public class FloatyAnimation : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public int yOffset;
    public float yScale;
    public float animationDuration;
    public float delay;
    
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence()
            .Insert(0, transform.DOMoveY(transform.position.y + yOffset, animationDuration).SetEase(animationCurve))
            .Insert(0, transform.DOScaleY(yScale, animationDuration).SetEase(animationCurve))
            .SetLoops(-1)
            .SetDelay(delay);
    }
}
