using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageTaker : MonoBehaviour
{
    // Start is called before the first frame update
    bool isdead=false;
    private bool isCoroutineExecuting = false;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isdead", isdead);
    }

     IEnumerator SetisDead()
    {
        if (isCoroutineExecuting)
        {
             yield break;
        } 
        isCoroutineExecuting = true;
        isdead =true;
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.6f);
        isCoroutineExecuting = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }

    public void Ondamage()
    {
        StartCoroutine(SetisDead());
    }
}
