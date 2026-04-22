using UnityEngine;
using R3;

public class BarPresenter : MonoBehaviour
{
    [SerializeField] private Bar _bar;
    [SerializeField] private SceneSwitcher _switcher;

    private void Start()
        => _switcher.ProgressUpdated.Subscribe(_bar.SetValue).AddTo(this);
}