using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FlyingNames : MonoBehaviour
{
    public Image[] names;
    public AnimationCurve xMovementCurve;
    public float delay;
    public int distance;
    public int animationDuration;
    
    void Start()
    {
        for (var i = 0; i < names.Length; i++)
        {
            var image = names[i];
            image.transform
                .DOMoveX(image.transform.position.x + distance, animationDuration)
                .SetDelay(i * delay)
                .SetEase(xMovementCurve);
        }
    }
}
