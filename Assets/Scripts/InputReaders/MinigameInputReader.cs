using UnityEngine.InputSystem;
using System;

public class MinigameInputReader : InputReader
{
    private InputSystem.MinigameActions _action;

    public event Action<float> TurnPerformed;
    public event Action EnterePerformed;
    public event Action CancelPerformed;

    private void Awake()
    {
        _action = new InputSystem().Minigame;
        Map = _action;
    }

    private void OnEnable()
    {
        _action.Turn.performed += InvokeTurn;
        _action.Enter.performed += InvokeEnter;
        _action.Cancel.performed += InvokeCancel;
    }

    private void OnDisable()
    {
        Deactivate();

        _action.Turn.performed -= InvokeTurn;
        _action.Enter.performed -= InvokeEnter;
        _action.Cancel.performed -= InvokeCancel;
    }

    private void InvokeTurn(InputAction.CallbackContext context)
        => TurnPerformed?.Invoke(_action.Turn.ReadValue<float>());

    private void InvokeEnter(InputAction.CallbackContext context)
        => EnterePerformed?.Invoke();

    private void InvokeCancel(InputAction.CallbackContext context)
        => CancelPerformed?.Invoke();
}