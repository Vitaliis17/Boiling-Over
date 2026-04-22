using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    private void Awake()
        => Lock();

    public void Unlock()
        => Cursor.lockState = CursorLockMode.None;

    public void Lock()
        => Cursor.lockState = CursorLockMode.Locked;
}