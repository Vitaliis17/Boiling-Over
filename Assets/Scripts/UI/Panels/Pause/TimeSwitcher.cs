using UnityEngine;

public class TimeSwitcher : MonoBehaviour
{
    private readonly int _defaultTime = 1;
    private readonly int _minTime = 0;

    private void Awake()
        => SetDefault();

    public void SetDefault()
        => Time.timeScale = _defaultTime;

    public void SetMin()
        => Time.timeScale = _minTime;
}