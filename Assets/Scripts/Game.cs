using UnityEngine;

public class Game
{
	private static Game instance = null;
	private Game()
	{
	}

	public static Game Instance
	{
		get
		{
			if (instance==null)
			{
				instance = new Game();
				instance.Initialize();
			}
			return instance;
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

	private void Initialize()
	{
		_gameModel = new GameModel();
		_gameSettings = Resources.Load<GameSettings>("GameSettings");
		_gameController = GameObject.FindWithTag(Tags.Main).GetComponent<GameController>();
	}
}
