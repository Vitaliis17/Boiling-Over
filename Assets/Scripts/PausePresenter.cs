using UnityEngine;

public class PausePresenter : MonoBehaviour 
{
    [SerializeField] private PauseSetter _pauseSetter;

    [SerializeField] private CursorLocker _cursorLocker;
    [SerializeField] private TimeSwitcher _timeSwitcher;

    private void OnEnable()
    {
        _pauseSetter.Activated += _cursorLocker.Unlock;
        _pauseSetter.Activated += _timeSwitcher.SetMin;

        _pauseSetter.Deactivated += _cursorLocker.Lock;
        _pauseSetter.Deactivated += _timeSwitcher.SetDefault;
    }

    private void OnDisable()
    {
        _pauseSetter.Activated -= _cursorLocker.Unlock;
        _pauseSetter.Activated -= _timeSwitcher.SetMin;

        _pauseSetter.Deactivated -= _cursorLocker.Lock;
        _pauseSetter.Deactivated -= _timeSwitcher.SetDefault;
    }
}