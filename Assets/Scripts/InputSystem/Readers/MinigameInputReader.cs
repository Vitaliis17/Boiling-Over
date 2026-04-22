using UnityEngine.InputSystem;
using System;
using R3;

public class MinigameInputReader : InputReader
{
    private InputSystem.MinigameActions _action;

    private readonly Subject<float> _turned = new();

    private readonly Subject<Unit> _entered = new();
    private readonly Subject<Unit> _cancelled = new();

    public Observable<float> Turned => _turned;

    public Observable<Unit> Entered => _entered;
    public Observable<Unit> Cancelled => _cancelled;

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

    private void OnDestroy()
    {
        _turned?.Dispose();
        _entered?.Dispose();
        _cancelled?.Dispose();
    }

    private void InvokeTurn(InputAction.CallbackContext context)
        => _turned.OnNext(context.ReadValue<float>());

    private void InvokeEnter(InputAction.CallbackContext context)
        => _entered.OnNext(Unit.Default);

    private void InvokeCancel(InputAction.CallbackContext context)
        => _cancelled.OnNext(Unit.Default);
}