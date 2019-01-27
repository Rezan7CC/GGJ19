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
    public int startPositionX;

	private void Start()
	{
		StartAnimation();
	}

	private void StartAnimation()
	{
		for (var i = 0; i < names.Length; i++)
		{
			var image = names[i];
			
			var startPos = image.transform.localPosition;
			startPos.x = startPositionX;
			image.transform.localPosition = startPos;
			
			image.transform
				.DOMoveX(image.transform.position.x + distance, animationDuration)
				.SetDelay(i * delay)
				.SetEase(xMovementCurve)
				.OnComplete(() => OnTweenEnd(image));
		}
	}

	private void OnTweenEnd(Image nameIndex)
	{
		if (nameIndex == names[names.Length - 1])
		{
			StartAnimation();
		}
	}
}
