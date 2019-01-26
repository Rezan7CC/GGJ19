using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour, IResetable
{
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
        Game.Instance.GameModel.SetScore(0);
        Game.Instance.GameModel.SetResourceAmount(0);
    }
}
