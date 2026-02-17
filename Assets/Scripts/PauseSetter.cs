using UnityEngine;
using System;

public class PauseSetter : MonoBehaviour 
{
    [SerializeField] private Transform _panel;
    
    [SerializeField] private GameInputReader _inputReader;

    private bool _isPause;

    public event Action Activated;
    public event Action Deactivated;

    private void Awake()
        => _isPause = false;

    private void OnEnable()
        => _inputReader.EscapePressed += SwitchState;

    private void OnDisable()
        => _inputReader.EscapePressed -= SwitchState;

    public void Deactivate()
    {
        _isPause = false;
        SetPanelActivity();

        Deactivated?.Invoke();
    }

    private void Activate()
    {
        _isPause = true;
        SetPanelActivity();

        Activated?.Invoke();
    }

    private void SwitchState()
    {
        if (_isPause)
        {
            Deactivate();
            return;
        }

        Activate();
    }

    private void SetPanelActivity()
        => _panel.gameObject.SetActive(_isPause);
}