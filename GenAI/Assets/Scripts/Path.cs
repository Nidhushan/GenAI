using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject[] pathpoints;
    public int numberofpoints;
    public float speed;
    private Vector3 actualposition;
    private int x;

    void Start()
    {
        x=0;
    }

    // Update is called once per frame
    void Update()
    {
        actualposition = obj.transform.position;
        obj.transform.position = Vector3.MoveTowards(actualposition,pathpoints[x].transform.position,speed*Time.deltaTime);        
        if(actualposition == pathpoints[x].transform.position && x==numberofpoints-1)
        {
            x=0;
        }
        else if(actualposition == pathpoints[x].transform.position && x!=numberofpoints-1)
        {
            x++;
        }

    }
}
