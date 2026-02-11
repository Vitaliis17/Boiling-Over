using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem _inputSystem;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;

    public event Action<Vector2> MovePerformed;
    public event Action<Vector2> LookPerformed;

    public event Action JumpingPerformed;
    public event Action InteractivePerformed;

    private void Awake()
        => _inputSystem = new();

    private void OnEnable()
    {
        _inputSystem.Player.Enable();

        _inputSystem.Player.Jump.performed += InvokeJumping;
        _inputSystem.Player.Interactive.performed += InvokeInteractive;
    }

    private void OnDisable()
    {
        _inputSystem.Player.Disable();

        _inputSystem.Player.Jump.performed -= InvokeJumping;
        _inputSystem.Player.Interactive.performed -= InvokeInteractive;
    }

    private void Update()
    {
        _moveDirection = _inputSystem.Player.Movement.ReadValue<Vector2>();
        _lookDirection = _inputSystem.Player.Look.ReadValue<Vector2>();
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