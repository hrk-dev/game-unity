using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string SceneName = "Test";

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    public void LoadGame()
    {
        GameData.Load();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
