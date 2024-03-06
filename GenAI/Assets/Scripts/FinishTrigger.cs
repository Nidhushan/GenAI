using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isCoroutineExecuting = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Finish");
            StartCoroutine(Finish());
        }
    }


     IEnumerator Finish()
    {
        if (isCoroutineExecuting)
        {
             yield break;
        } 
        isCoroutineExecuting = true;
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.4f);
        isCoroutineExecuting = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameComplete");
    }
}
