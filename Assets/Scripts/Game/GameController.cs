using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IResetable
{
    private Game _gameInstance;

    private void Awake()
    {
        _gameInstance = Game.Instance;
        _gameInstance.Restart();
    }

    private void Start()
    {
        _gameInstance.GameModel.OnScoreChange += OnScoreChanged;
        _gameInstance.GameModel.OnHealthChange += OnHealthChanged;

        _gameInstance.GameSignals.OnWin += OnWin;
        _gameInstance.GameSignals.OnGameOver += OnGameOver;
        
        RestartGame();
    }

    private void OnGameOver()
    {
        SceneManager.LoadScene(0);
    }

    private void OnWin()
    {
        
    }

    private void OnHealthChanged(int health)
    {
        if (health == 0 && _gameInstance.GameSignals.OnGameOver != null)
        {
            Debug.Log("GAME OVER!!!");
            _gameInstance.GameSignals.OnGameOver();
        }
    }

    private void OnScoreChanged(int score)
    {
        if (score >= _gameInstance.GameSettings.segmentScores[_gameInstance.GameSettings.segmentScores.Length - 1] &&
            _gameInstance.GameSignals.OnWin != null)
        {
            Debug.Log("WIN!!!");
            _gameInstance.GameSignals.OnWin();
        }
    }

    public void RestartGame()
    {
        IEnumerable<IResetable> resetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();
        foreach (var resetable in resetables) {
            resetable.Reset();
        }
    }

    public void Reset()
    {
        _gameInstance.GameModel.SetScore(0);
        _gameInstance.GameModel.SetResourceAmount(0);
        _gameInstance.GameModel.SetHealth(_gameInstance.GameSettings.maxHealth);
    }
}
