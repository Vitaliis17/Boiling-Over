using UnityEngine;
using R3;

public class PauseSetter : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private PlayerInputReader _inputReader;

    private readonly ReactiveProperty<bool> _isPause = new(false);

    public Observable<bool> PauseStateChanged => _isPause;

    private void Start()
        => _inputReader.Paused.Subscribe(_ => SwitchState()).AddTo(this);

    private void OnDestroy()
        => _isPause?.Dispose();

    private void SwitchState()
    {
        bool reverseActivity = _isPause.Value == false;
        _isPause.Value = reverseActivity;

        SetPanelActivity();
    }

    private void SetPanelActivity()
        => _panel.gameObject.SetActive(_isPause.Value);
}