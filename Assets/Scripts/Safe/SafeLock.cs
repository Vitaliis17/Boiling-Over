using UnityEngine;
using System;

public class SafeLock : MonoBehaviour
{
    [SerializeField] CodeData _code;

    private int _currentValue;
    private int _currentIndex;

    public event Action<int> Changed;

    public event Action Turned;
    public event Action Opened;
    public event Action Reseted;

    private void Awake()
        => ResetLock();

    public void Turn(float direction)
    {
        int sign = Math.Sign(direction);

        IncreaseCurrentValue(sign);

        Turned?.Invoke();
    }

    public void Enter()
    {
        if(_code.IsCorrectValue(_currentValue, _currentIndex) == false)
        {
            ResetLock();

            return;
        }

        _currentIndex++;
        ResetValue();

        if (_code.CodeIndexAmount == _currentIndex)
            Open();
    }

    private void Open()
    {
        Opened?.Invoke();

        gameObject.SetActive(false);
    }

    private void IncreaseCurrentValue(int value)
    {
        _currentValue += value;

        Changed?.Invoke(value);
    }

    private void ResetLock()
    {
        ResetValue();
        _currentIndex = 0;
    }

    private void ResetValue()
    {
        const int NegativeSign = -1;

        if (_currentValue == 0)
            return;

        IncreaseCurrentValue(NegativeSign * _currentValue);
        Reseted?.Invoke();
    }
}