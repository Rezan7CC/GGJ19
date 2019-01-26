using UnityEngine;

public class TriggerCollisionInvoker : MonoBehaviour
{
	public TriggerCollisionType CollisionType;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals(Tags.Player) && Game.Instance.GameSignals.OnTriggerCollisionEnter != null)
		{
			Debug.Log("Enter " + CollisionType);
			Game.Instance.GameSignals.OnTriggerCollisionEnter(CollisionType);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals(Tags.Player) && Game.Instance.GameSignals.OnTriggerCollisionExit != null)
		{
			Debug.Log("Exit " + CollisionType);
			Game.Instance.GameSignals.OnTriggerCollisionExit(CollisionType);
		}
	}
}
