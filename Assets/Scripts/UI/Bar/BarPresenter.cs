using UnityEngine;

public class BarPresenter : MonoBehaviour
{
    [SerializeField] private Bar _bar;
    [SerializeField] private SceneSwitcher _switcher;

    private void OnEnable()
        => _switcher.ProgressUpdated += _bar.SetValue;

    private void OnDisable()
        => _switcher.ProgressUpdated -= _bar.SetValue;
}