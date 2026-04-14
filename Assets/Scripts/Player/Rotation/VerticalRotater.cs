using UnityEngine;

public class VerticalRotater : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private LookRotationData _rotationData;

    private LookRotater _rotater;

    private void Awake()
        => _rotater = new(_rotationData, transform);
    
    private void OnEnable()
        => _inputReader.LookPerformed += _rotater.RotateY;

    private void OnDisable()
        => _inputReader.LookPerformed -= _rotater.RotateY;
}