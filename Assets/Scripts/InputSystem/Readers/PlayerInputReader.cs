using UnityEngine;
using UnityEngine.InputSystem;
using R3;

public class PlayerInputReader : InputReader
{
    private InputSystem.PlayerActions _action;

    private readonly Subject<Vector2> _moved = new();
    private readonly Subject<Vector2> _looked = new();

    private readonly Subject<Unit> _jumped = new();
    private readonly Subject<Unit> _interacted = new();
    private readonly Subject<Unit> _paused = new();

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;

    public Observable<Vector2> Moved => _moved;
    public Observable<Vector2> Looked => _looked;

    public Observable<Unit> Jumped => _jumped;
    public Observable<Unit> Interacted => _interacted;
    public Observable<Unit> Paused => _paused;

    private void Awake()
    {
        _action = new InputSystem().Player;
        Map = _action;
    }

    private void Update()
    {
        _moveDirection = _action.Movement.ReadValue<Vector2>();
        _lookDirection = _action.Look.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _moved.OnNext(_moveDirection);
        _looked.OnNext(_lookDirection);
    }

    private void OnEnable()
    {
        _action.Jump.performed += InvokeJumping;
        _action.Interactive.performed += InvokeInteractive;
        _action.Pause.performed += InvokePause;
    }

    private void OnDisable()
    {
        Deactivate();

        _action.Jump.performed -= InvokeJumping;
        _action.Interactive.performed -= InvokeInteractive;
        _action.Pause.performed -= InvokePause;
    }

    private void OnDestroy()
    {
        _moved?.Dispose();
        _looked?.Dispose();

        _jumped?.Dispose();
        _interacted?.Dispose();
        _paused?.Dispose();
    }

    private void InvokeJumping(InputAction.CallbackContext context)
        => _jumped.OnNext(Unit.Default);

    private void InvokeInteractive(InputAction.CallbackContext context)
        => _interacted.OnNext(Unit.Default);

    private void InvokePause(InputAction.CallbackContext context)
        => _paused.OnNext(Unit.Default);
}