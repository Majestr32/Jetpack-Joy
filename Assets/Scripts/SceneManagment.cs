using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    bool _isPreviousSceneNightMode = false;
    public static SceneManagment Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene(sceneName: "Game over");
    }
    public void RestartGame()
    {
        if (_isPreviousSceneNightMode)
        {
            GoToNightMode();
        }
        else
        {
            GoToGameMode();
        }
    }
    public void GoToGameMode()
    {
        Debug.Log("Clicked game");
        _isPreviousSceneNightMode = false;
        SceneManager.LoadScene(sceneName: "Game");
    }
    public void GoToNightMode()
    {
        _isPreviousSceneNightMode = true;
        SceneManager.LoadScene(sceneName: "Game DARK");
    }

}
