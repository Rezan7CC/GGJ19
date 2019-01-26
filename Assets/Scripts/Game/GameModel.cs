public class GameModel
{
	private int _score;
	public delegate void ScoreChange(int score);
	public ScoreChange OnScoreChange;

	public void SetScore(int score)
	{
		this._score = score;
		if (OnScoreChange != null)
		{
			OnScoreChange(this._score);
		}
	}

	public void IncreaseScore(int score)
	{
		this._score += score;
		if (OnScoreChange != null)
		{
			OnScoreChange(this._score);
		}
	}
	
	private ControlMode _controlMode;
	public delegate void ControlsModeChange(ControlMode controlMode);
	public ControlsModeChange OnControlModeChanged;

	public void SetControlMode(ControlMode controlMode)
	{
		_controlMode = controlMode;
		if (OnControlModeChanged != null)
		{
			OnControlModeChanged(_controlMode);
		}
	}

	public ControlMode GetControlMode()
	{
		return _controlMode;
	}
}
