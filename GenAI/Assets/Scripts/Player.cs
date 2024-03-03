using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputActionReference movement,pointerPosition;
    
    private AgentMover agentMover;

    private Vector2 pointerInput, movementInput;
    void Awake()
    {
        agentMover = GetComponent<AgentMover>();
    }

    // Update is called once per frame
    void Update()
    {
        pointerInput = GetpointerInput();
        movementInput = movement.action.ReadValue<Vector2>();
        agentMover.MovementInput = movementInput;
    }

    private Vector2 GetpointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
