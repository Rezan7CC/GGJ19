using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Custom/GameSettings")]
public class GameSettings : ScriptableObject
{
	[Header("Scoring")]
	public int[] segmentScores;
	
	[Header("Resources")]
	public int resourceCapacity;
	public int collectResouceSpeed;
	
	[Header("Health")]
	public int maxHealth;
	public int astroidDamage;
}
