using System.Threading;
using UnityEngine;
using R3;

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

    private void Start()
        => _clipsPlayer.Stepped.Subscribe(data => PlayFootStep(data.Item1, data.Item2, data.Item3)).AddTo(this);

    private void OnEnable()
        => _tokenSource = new();

    private void OnDisable()
    {
        _tokenSource.Cancel();
        _tokenSource.Dispose();
    }

    private async void PlayFootStep(AudioClip clip, Vector3 position, float volume)
    {
        FootStep footStep = _spawner.GetElement();

        footStep.transform.position = position;
        footStep.Play(clip, volume);

        bool isCancel = await _timer.WaitSeconds(clip.length, _tokenSource.Token).SuppressCancellationThrow();

        if (isCancel)
            return;

        _spawner.ReleaseElement(footStep);
    }
}