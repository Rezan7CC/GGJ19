using UnityEngine;

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

	private int _resourceAmount;
	public delegate void ResourceAmountChange(int amount);
	public ResourceAmountChange OnResourceAmountChange;

	public void SetResourceAmount(int amount)
	{
		_resourceAmount = amount;
		if (OnResourceAmountChange != null)
		{
			OnResourceAmountChange(_resourceAmount);
		}
	}

	public void IncreaseResourceAmount(int amount)
	{
		_resourceAmount = Mathf.Min(_resourceAmount + amount, Game.Instance.GameSettings.resourceCapacity);
		if (OnResourceAmountChange != null)
		{
			OnResourceAmountChange(_resourceAmount);
		}
	}

	public void DeliverResources()
	{
		if (_resourceAmount > 0)
		{
			IncreaseScore(_resourceAmount);
			SetResourceAmount(0);
		}
	}
}
