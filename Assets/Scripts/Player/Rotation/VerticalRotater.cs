using UnityEngine;
using R3;
using System;

public class VerticalRotater : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private LookRotationData _rotationData;

    private LookRotater _rotater;
    private DisposableBag _bag = new();

    private void Awake()
        => _rotater = new(_rotationData, transform);

    private void OnEnable()
        => _inputReader.Looked.Subscribe(direction => _rotater.RotateY(direction)).AddTo(ref _bag);

    private void OnDisable()
        => _bag.Clear();

    private void OnDestroy()
        => _bag.Dispose();
}