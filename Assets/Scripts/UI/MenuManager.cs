using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;

    void Start()
    {
        Close();
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Pause()
    {
        if (pauseMenu == null) return;

        pauseMenu.alpha = 1f;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;

        Time.timeScale = 0f; 
    }

   
    public void Close()
    {
        if (pauseMenu == null) return;

        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;

        Time.timeScale = 1f; 
    }
}