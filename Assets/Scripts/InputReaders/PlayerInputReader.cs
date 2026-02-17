using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private InputSystem.PlayerActions _action;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;

    public event Action<Vector2> MovePerformed;
    public event Action<Vector2> LookPerformed;

    public event Action JumpingPerformed;
    public event Action InteractivePerformed;

    private void Awake()
        => _action = new InputSystem().Player;

    private void OnEnable()
    {
        _action.Enable();

        _action.Jump.performed += InvokeJumping;
        _action.Interactive.performed += InvokeInteractive;
    }

    private void OnDisable()
    {
        _action.Disable();

        _action.Jump.performed -= InvokeJumping;
        _action.Interactive.performed -= InvokeInteractive;
    }

    private void Update()
    {
        _moveDirection = _action.Movement.ReadValue<Vector2>();
        _lookDirection = _action.Look.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_moveDirection.sqrMagnitude > 0)
            MovePerformed?.Invoke(_moveDirection);

        if (_lookDirection.sqrMagnitude > 0)
            LookPerformed?.Invoke(_lookDirection);
    }

    private void InvokeJumping(InputAction.CallbackContext context)
        => JumpingPerformed?.Invoke();

    private void InvokeInteractive(InputAction.CallbackContext context)
        => InteractivePerformed?.Invoke();
}