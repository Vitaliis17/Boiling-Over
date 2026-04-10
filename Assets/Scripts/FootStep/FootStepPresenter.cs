using System.Threading;
using UnityEngine;

public class FootStepPresenter : MonoBehaviour
{
    [SerializeField] private FootClipsPlayer _clipsPlayer;

    [SerializeField] private FootStep _prefab;
    [SerializeField] private Transform _container;

    private FootStepSpawner _spawner;
    private Timer _timer;

    private CancellationTokenSource _tokenSource;

    private void Awake()
    {
        _spawner = new(_prefab, _container);
        _timer = new();
    }

    private void OnEnable()
    {
        _tokenSource = new();

        _clipsPlayer.Stepped += PlayFootStep;
    }

    private void OnDisable()
    {
        _clipsPlayer.Stepped -= PlayFootStep;

        _tokenSource.Cancel();
        _tokenSource.Dispose();
    }

    private async void PlayFootStep(AudioClip clip, Vector3 position, float volume)
    {
        FootStep footStep = _spawner.GetElement();

        footStep.transform.position = position;
        footStep.Play(clip, volume);

        try
        {
            await _timer.WaitSeconds(clip.length, _tokenSource.Token);
        }
        finally
        {
            _spawner.ReleaseElement(footStep);
        }
    }
}