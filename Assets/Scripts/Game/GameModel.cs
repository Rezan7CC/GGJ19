public class GameModel
{
	private int score;
	
	public delegate void ScoreChange(int score);

	public ScoreChange OnScoreChange;

	public void SetScore(int score)
	{
		this.score = score;
		if (OnScoreChange != null)
		{
			OnScoreChange(this.score);
		}
	}

	public void IncreaseScore(int score)
	{
		this.score += score;
		if (OnScoreChange != null)
		{
			OnScoreChange(this.score);
		}
	}
}
