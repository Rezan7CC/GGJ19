using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Game.Instance.GameSignals.OnGameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGameOver()
    {
        gameObject.SetActive(true);    
        StartCoroutine(LoseSequence());
    }

    IEnumerator LoseSequence()
    {
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene(0);
        yield return new WaitForEndOfFrame();
    }
}
