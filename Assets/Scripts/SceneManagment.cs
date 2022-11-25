using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
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
        DontDestroyOnLoad(gameObject);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene(sceneName: "Game over");
    }
    public void GoToGameMode()
    {
        SceneManager.LoadScene(sceneName: "Game");
    }
    public void GoToNightMode()
    {
        SceneManager.LoadScene(sceneName: "Game DARK");
    }

}
