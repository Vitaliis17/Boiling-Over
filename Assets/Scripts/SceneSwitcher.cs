using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public event Action<float> ProgressUpdated;

    private Coroutine _coroutine;

    private void OnDisable()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void LoadGame()
        => _coroutine = StartCoroutine(LoadSceneAsync(SceneNames.Game));

    public void LoadMenu()
        => _coroutine = StartCoroutine(LoadSceneAsync(SceneNames.Menu));

    private IEnumerator LoadSceneAsync(SceneNames scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)scene);

        float progress;

        while(operation.isDone == false)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            ProgressUpdated?.Invoke(progress);

            yield return null;
        }
    }
}
