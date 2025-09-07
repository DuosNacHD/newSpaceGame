using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    InputSystem_Actions action;
    public event EventHandler SprintOn;
    public event EventHandler SprintOff;
    public event EventHandler Attack;

    private void Awake()
    {
        action = new InputSystem_Actions();
        action.Player.Enable();
        action.Player.Sprint.performed += Sprint_performed;
        action.Player.Sprint.canceled += Sprint_canceled;
        action.Player.Attack.performed += Attack_performed;
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        Attack?.Invoke(this,EventArgs.Empty);
    }

    private void Sprint_canceled(InputAction.CallbackContext context)
    {
        SprintOff?.Invoke(this,EventArgs.Empty);
    }

    private void Sprint_performed(InputAction.CallbackContext context)
    {
        SprintOn?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetInputVector()
    {
        return action.Player.Move.ReadValue<Vector2>().normalized;
    }
}
