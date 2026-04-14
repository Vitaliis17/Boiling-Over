using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetValue(float value)
        => _slider.value = value;
}