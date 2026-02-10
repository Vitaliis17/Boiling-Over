using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float _closeRotatation;
    [SerializeField] private float _openRotatation;

    [SerializeField, Min(0)] private float _animationDuration;

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
        DOLocalRotation(Vector3.up * _openRotatation);

        _currentState = DoorStates.Opened;
    }

    private void Close()
    {
        DOLocalRotation(Vector3.up * _closeRotatation);

        _currentState = DoorStates.Closed;
    }

    private void DOLocalRotation(Vector3 rotation)
        => transform.DOLocalRotate(rotation, _animationDuration);
}