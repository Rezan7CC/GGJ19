using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class ResourcePlanetSnapIndicator : MonoBehaviour, IResetable
{
	// Start is called before the first frame update
	void Start()
	{
		Game.Instance.GameSignals.OnTriggerCollisionEnter += OnTriggerCollisionEnter;
		Game.Instance.GameSignals.OnTriggerCollisionExit += OnTriggerCollisionExit;
	}

	private void OnTriggerCollisionExit(TriggerCollisionType type)
	{
		if (type == TriggerCollisionType.ResourcePlanet)
		{
			Hide();
		}
	}

	private void OnTriggerCollisionEnter(TriggerCollisionType type)
	{
		if (type == TriggerCollisionType.ResourcePlanet)
		{
			Show();
		}
	}

	private void Show()
	{
		gameObject.transform.DOScale(1, 0.15f).SetEase(Ease.OutBack);
	}

	private void Hide(float duration = 0.15f)
	{
		gameObject.transform.DOScale(0, duration);
	}

	public void Reset()
	{
		Hide(0);
	}
}
