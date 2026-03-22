using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private float tweenDuration = 0.25f;

    void Awake()
    {
        // ẩn menu lúc bắt đầu
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
        pauseMenu.transform.localScale = Vector3.zero;
    }

    public void Pause()
    {
        if (pauseMenu == null) return;

        StopAllCoroutines();
        StartCoroutine(ShowMenuCoroutine());

        Time.timeScale = 0f; // pause game
    }

    public void Close()
    {
        if (pauseMenu == null) return;

        StopAllCoroutines();
        StartCoroutine(HideMenuCoroutine());

        Time.timeScale = 1f; // resume game
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    private IEnumerator ShowMenuCoroutine()
    {
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;

        float t = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        while (t < tweenDuration)
        {
            t += Time.unscaledDeltaTime;
            float normalized = t / tweenDuration;

            pauseMenu.alpha = Mathf.Lerp(0f, 1f, normalized);
            pauseMenu.transform.localScale = Vector3.Lerp(startScale, targetScale, normalized);

            yield return null;
        }

        pauseMenu.alpha = 1f;
        pauseMenu.transform.localScale = Vector3.one;
    }

    private IEnumerator HideMenuCoroutine()
    {
        float t = 0f;
        Vector3 startScale = pauseMenu.transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (t < tweenDuration)
        {
            t += Time.unscaledDeltaTime;
            float normalized = t / tweenDuration;

            pauseMenu.alpha = Mathf.Lerp(1f, 0f, normalized);
            pauseMenu.transform.localScale = Vector3.Lerp(startScale, targetScale, normalized);

            yield return null;
        }

        pauseMenu.alpha = 0f;
        pauseMenu.transform.localScale = Vector3.zero;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
    }
}