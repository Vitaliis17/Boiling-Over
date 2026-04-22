using UnityEngine;
using R3;
using System;

public class SafeLockPresenter : MonoBehaviour
{
    [SerializeField] private SafeLock _lock;
    [SerializeField] private AnimationRotaterData _rotaterData;
    [SerializeField] private MinigameInputReader _inputReader;
    [SerializeField] private SafeAudioPlayer _audioPlayer;

    [SerializeField] private Rigidbody _rigidbody;

    private Rotater _rotater;

    private void Awake()
        => _rotater = new(_rotaterData, _rigidbody);

    private void Start()
    {
        _lock.Turned.Subscribe(sign =>
        {
            _rotater.Rotate(sign);
            _audioPlayer.PlayRotation();
        }).AddTo(this);

        _lock.Reset.Subscribe(rotateAmount =>
        {
            _rotater.Rotate(rotateAmount);
            _audioPlayer.PlayReset();
        }).AddTo(this);

        _lock.Opened.Subscribe(_ => _audioPlayer.PlayOpen()).AddTo(this);

        _inputReader.Entered.Subscribe(_ => _lock.Enter()).AddTo(this);
        _inputReader.Turned.Select(direction => Math.Sign(direction)).Subscribe(direction => _lock.Turn(direction)).AddTo(this);
    }
}