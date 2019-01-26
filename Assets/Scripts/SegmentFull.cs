using UnityEngine;

public class SegmentFull : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag.Equals(Tags.Astroid) && Game.Instance.GameSignals.OnAstroidHitSegment != null)
		{
			Game.Instance.GameSignals.OnAstroidHitSegment(other.gameObject);
		}
	}
}
