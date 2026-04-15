using UnityEngine;

public class WinnerPresenter : MonoBehaviour
{
    [SerializeField] private WinChecker _checker;
    [SerializeField] private WinnerSetter _winnerSetter;

    [SerializeField] private CursorLocker _cursorLocker;
    [SerializeField] private TimeSwitcher _timeSwitcher;

    private void OnEnable()
    {
        _checker.PlayerTriggered += _winnerSetter.Activate;

        _winnerSetter.Activated += _cursorLocker.Unlock;
        _winnerSetter.Activated += _timeSwitcher.SetMin;
    }

    private void OnDisable()
    {
        _checker.PlayerTriggered -= _winnerSetter.Activate;

        _winnerSetter.Activated -= _cursorLocker.Unlock;
        _winnerSetter.Activated -= _timeSwitcher.SetMin;
    }
}