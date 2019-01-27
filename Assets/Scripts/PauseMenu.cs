using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool disablePause;
    
    public GameObject PM;
    private void Start()
    {
        Game.Instance.GameSignals.OnWin += OnWin;
        PM.SetActive(false);
    }

    private void OnWin()
    {
        disablePause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!PM.activeSelf && !disablePause)
            {
                Game.Instance.GameModel.InGameTimeScale = 0;
                PM.SetActive(true);
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Unpause()
    {
        Game.Instance.GameModel.InGameTimeScale = 1;
        PM.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
