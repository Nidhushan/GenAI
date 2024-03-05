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
    private Animator animator;
    private int currentPointIndex = 0;

    void Start()
    {
        x=0;
        animator = obj.GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        actualposition = obj.transform.position;
        obj.transform.position = Vector3.MoveTowards(actualposition,pathpoints[x].transform.position,speed*Time.deltaTime);
        if (Vector3.Distance(obj.transform.position, pathpoints[currentPointIndex].transform.position) < 0.1f)
        {
            // Move to the next path point
            currentPointIndex = (currentPointIndex + 1) % numberofpoints;
            // Calculate the direction to the next point
            Vector3 direction = (pathpoints[currentPointIndex].transform.position - obj.transform.position).normalized;

            // Set animator parameters based on direction
            animator.SetFloat("Forward", Mathf.Clamp(direction.y, -1f, 1f));
            animator.SetFloat("Right", Mathf.Clamp(direction.x, -1f, 1f));
            animator.SetFloat("Backward", Mathf.Clamp(-direction.y, 0f, 1f)); // Backward
            animator.SetFloat("Left", Mathf.Clamp(-direction.x, 0f, 1f)); // Left

        }
        if (actualposition == pathpoints[x].transform.position && x==numberofpoints-1)
        {
            x=0;
        }
        else if(actualposition == pathpoints[x].transform.position && x!=numberofpoints-1)
        {
            x++;
        }

    }
}
