namespace DefaultNamespace.Game
{
	public class GameSignals
	{
		public delegate void TriggerCollision(TriggerCollisionType type);
		public TriggerCollision OnTriggerEnter;
		public TriggerCollision OnTriggerExit;
	}
}