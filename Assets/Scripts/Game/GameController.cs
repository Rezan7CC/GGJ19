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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Game.Instance.GameModel.IncreaseScore(20);
        }
    }

    public void Reset()
    {
        Game.Instance.GameModel.SetScore(0);
    }
}
