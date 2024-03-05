using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject Target;
    [SerializeField]
    public float distance=3.0f;

    [SerializeField]
    public string Soundname;

    [SerializeField]
    public float waittime = 2.0f;
    bool canplay=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Target.transform.position-gameObject.transform.position).magnitude<distance)
        {
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        if(canplay == true)
        {
            yield return new WaitForSeconds(waittime);;
        }
        else
        {
            canplay = true;
            Debug.Log("Sound");
            AudioManager.instance.Play(Soundname);
            yield return new WaitForSeconds(waittime); 
            canplay = false;
        }
    }
}
