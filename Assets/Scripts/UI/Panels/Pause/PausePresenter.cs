using R3;
using UnityEngine;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private PauseSetter _pauseSetter;

    [SerializeField] private CursorLocker _cursorLocker;
    [SerializeField] private TimeSwitcher _timeSwitcher;

    private void Start()
        => _pauseSetter.PauseStateChanged.Subscribe(isPause => ChangePauseState(isPause)).AddTo(this);

    private void ChangePauseState(bool isPause)
    {
        if (isPause)
        {
            _cursorLocker.Unlock();
            _timeSwitcher.SetMin();
        }
        else
        {
            _cursorLocker.Lock();
            _timeSwitcher.SetDefault();
        }
    }
}