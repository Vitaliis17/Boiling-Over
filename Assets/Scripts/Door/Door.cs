using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorData _data;

    private CustomeLayerMasks _mask;
    private DoorStates _currentState;

    private void Awake()
    {
        _mask = CustomeLayerMasks.Interactable;
        _currentState = DoorStates.Closed;

        gameObject.layer = (int)_mask;
    }

    public void Interact()
    {
        switch (_currentState)
        {
            case DoorStates.Closed:
                Open();
                break;

            case DoorStates.Opened:
                Close();
                break;
        }
    }

    private void Open()
    {
        transform.localRotation = Quaternion.Euler(_data.OpenRotation);

        _currentState = DoorStates.Opened;
    }

    private void Close()
    {
        transform.localRotation = Quaternion.Euler(_data.CloseRotation);

        _currentState = DoorStates.Closed;
    }
}