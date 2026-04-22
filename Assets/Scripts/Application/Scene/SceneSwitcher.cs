using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SceneSwitcher : MonoBehaviour
{
    private readonly ReactiveProperty<float> _progress = new();

    private CancellationTokenSource _source;

    public Observable<float> ProgressUpdated => _progress;

    private void OnEnable()
        => _source = new();

    private void OnDisable()
    {
        _source?.Cancel();
        _source?.Dispose();
    }

    private void OnDestroy()
        => _progress?.Dispose();

    public void LoadGame()
        => LoadSceneAsync(SceneNames.Game, _source.Token).Forget();

    public void LoadMenu()
        => LoadSceneAsync(SceneNames.Menu, _source.Token).Forget();

    private async UniTaskVoid LoadSceneAsync(SceneNames scene, CancellationToken token)
    {
        const int NoProgress = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync((int)scene);
        _progress.Value = NoProgress;

        while(operation.isDone == false)
        {
            _progress.Value = Mathf.Clamp01(operation.progress / 0.9f);

            await UniTask.NextFrame(token).SuppressCancellationThrow();
        }
    }
}
