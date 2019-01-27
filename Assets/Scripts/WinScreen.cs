using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Game.Instance.GameSignals.OnWin += OnWin;
    }

    private void OnWin()
    {
        gameObject.SetActive(true);
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(20.0f);
        SceneManager.LoadScene(0);
        yield return new WaitForEndOfFrame();
    }
}
