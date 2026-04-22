using UnityEngine;
using R3;

public class WinnerPresenter : MonoBehaviour
{
    [SerializeField] private WinChecker _checker;
    [SerializeField] private WinnerSetter _winnerSetter;

    [SerializeField] private CursorLocker _cursorLocker;
    [SerializeField] private TimeSwitcher _timeSwitcher;

    private void Start()
    {
        _checker.Triggered.Subscribe(_ => _winnerSetter.Activate()).AddTo(this);

        _winnerSetter.Activated.Subscribe(_ =>
        {
            _cursorLocker.Unlock();
            _timeSwitcher.SetMin();
        }
        ).AddTo(this);
    }
}