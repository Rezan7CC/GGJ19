using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour, IResetable
{
    private Game _gameInstance;

    private void Awake()
    {
        _gameInstance = Game.Instance;
    }

    private void Start()
    {
        RestartGame();
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
