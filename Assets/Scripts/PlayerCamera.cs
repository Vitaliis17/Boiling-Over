using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Awake()
        => _camera.forceIntoRenderTexture = true;
}
