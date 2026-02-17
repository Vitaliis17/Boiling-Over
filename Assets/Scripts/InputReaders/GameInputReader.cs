using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInputReader : MonoBehaviour
{
    private InputSystem.GameActions _action;

    public event Action EscapePressed;

    private void Awake()
        => _action = new InputSystem().Game;

    private void OnEnable()
    {
        _action.Enable();

        _action.Pause.performed += InvokeEscapePressed;
    }

    private void OnDisable()
    {
        _action.Disable();

        _action.Pause.performed -= InvokeEscapePressed;
    }

    private void InvokeEscapePressed(InputAction.CallbackContext context)
        => EscapePressed?.Invoke();
}