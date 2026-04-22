using UnityEngine;
using R3;

public class SafeLock : MonoBehaviour
{
    [SerializeField] CodeData _code;

    private int _currentIndex;
    private int _currentValue;

    private readonly Subject<int> _turned = new();
    private readonly Subject<int> _reset = new();
    private readonly Subject<Unit> _opened = new();

    public Observable<int> Turned => _turned;
    public Observable<int> Reset => _reset;
    public Observable<Unit> Opened => _opened;

    private void Awake()
        => ResetLock();

    private void OnDestroy()
    {
        _turned?.Dispose();
        _opened?.Dispose();
        _reset?.Dispose();
    }

    public void Turn(int direction)
    {
        _currentValue += direction;

        _turned.OnNext(direction);
    }

    public void Enter()
    {
        if(_code.IsCorrectValue(_currentValue, _currentIndex) == false)
        {
            ResetLock();

            return;
        }

        _currentIndex++;
        ResetCurrentValue();

        if (_code.CodeIndexAmount == _currentIndex)
            Open();
    }

    private void Open()
    {
        _opened.OnNext(Unit.Default);

        gameObject.SetActive(false);
    }

    private void ResetLock()
    {
        ResetCurrentValue();
        _currentIndex = 0;
    }

    private void ResetCurrentValue()
    {
        const int NoValue = 0;
        const int NegativeSign = -1;

        if (_currentValue == NoValue)
            return;

        _reset.OnNext(NegativeSign * _currentValue);
        _currentValue = NoValue;

    }
}