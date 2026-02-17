using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadGame()
        => LoadScene(SceneNames.Game);

    public void LoadMenu()
        => LoadScene(SceneNames.Menu);

    private void LoadScene(SceneNames scene)
        => SceneManager.LoadScene((int)scene);
}
