using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    public float Duration = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(Duration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Die(float duration)
    {
        //play your sound
        yield return new WaitForSeconds(duration); //waits 3 seconds
        Destroy(gameObject); //this will work after 3 seconds.
    }
}
