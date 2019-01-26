using UnityEngine;

public class Game
{
	private static Game _instance = null;
	private Game()
	{
	}

	public static Game Instance
	{
		get
		{
			if (_instance==null)
			{
				_instance = new Game();
				_instance.Initialize();
			}
			return _instance;
		}
	}
	
	private GameController _gameController;
	public GameController GameController
	{
		get { return _gameController; }
	}

	private GameModel _gameModel;
	public GameModel GameModel
	{
		get { return _gameModel; }
	}

	private GameSettings _gameSettings;
	public GameSettings GameSettings
	{
		get { return _gameSettings; }
	}
	
	private GameSignals _gameSignals;
	public GameSignals GameSignals
	{
		get { return _gameSignals; }
	}

	private void Initialize()
	{
		_gameModel = new GameModel();
		_gameSignals = new GameSignals();
		_gameSettings = Resources.Load<GameSettings>("GameSettings");
		_gameController = GameObject.FindWithTag(Tags.Main).GetComponent<GameController>();
	}

	public void Restart()
	{
		Initialize();
	}
}
