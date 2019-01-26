using UnityEngine;

public class GameSignals
{
	public delegate void TriggerCollision(TriggerCollisionType type);
	public TriggerCollision OnTriggerCollisionEnter;
	public TriggerCollision OnTriggerCollisionExit;
	
	public delegate void IntTrigger(int value);
	public IntTrigger OnHomeSegmentCountChanged;

	public delegate void GameObjectTrigger(GameObject segment);
	public GameObjectTrigger OnAstroidHitSegment;

	public delegate void VoidTrigger();
	public VoidTrigger OnGameOver;
	public VoidTrigger OnWin;
}
