using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputSystem_Actions action;

    private void Awake()
    {
        action = new InputSystem_Actions();
        action.Player.Enable();
    }
    private void Update()
    {
        Vector2 inputVector = action.Player.Move.ReadValue<Vector2>().normalized;
        
    }
}
