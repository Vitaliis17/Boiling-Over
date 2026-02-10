using UnityEngine;

public class VerticalRotater : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RotationData _rotationData;

    private Rotater _rotater;
    
    private void Awake()
        => _rotater = new(_rotationData, transform);

    private void OnEnable()
        => _inputReader.LookPerformed += _rotater.RotateY;

    private void OnDisable()
        => _inputReader.LookPerformed -= _rotater.RotateY;
}