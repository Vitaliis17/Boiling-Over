using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputReader : MonoBehaviour
{
    protected InputActionMap Map;

    public string Name => Map?.name;

    public void Activate()
        => Map?.Enable();

    public void Deactivate()
        => Map?.Disable();
}