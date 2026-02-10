using UnityEngine;

public class VerticalRotater : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    [SerializeField] private float _sensitivity;

    private Rotater _rotater;
    
    private void Awake()
        => _rotater = new(_sensitivity, _maxY, _minY);

    private void OnEnable()
        => _inputReader.LookPerformed += RotateY;

    private void OnDisable()
        => _inputReader.LookPerformed -= RotateY;

    private void RotateY(Vector2 direction)
        => _rotater.RotateY(transform, direction);
}