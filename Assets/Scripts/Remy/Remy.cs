using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Remy : MonoBehaviour
{
    [SerializeField] private ZoneChecker _sightZone;

    private void Awake()
        => GetComponent<Rigidbody>().freezeRotation = true;
}