using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gate;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.ShowPrompt("Get the artifacts to open the gate");
    }

    public void OpenGate()
    {
        UIManager.Instance.ShowPrompt("The gate has opened.");
        Destroy(gate);
    }
}
