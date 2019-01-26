using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        RestartGame();
    }

    public void RestartGame()
    {
        var resetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();
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
}
