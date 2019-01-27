using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PM;
    private void Start()
    {
        PM.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
//            Time.timeScale = 0;
            PM.SetActive(true);
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1; 
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
