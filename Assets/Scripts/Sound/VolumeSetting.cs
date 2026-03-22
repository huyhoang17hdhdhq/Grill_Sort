using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;

    [Header("=== MUSIC ===")]
    [SerializeField] private Button musicButton;
    [SerializeField] private GameObject musicOnUI;
    [SerializeField] private GameObject musicOffUI;

    [Header("=== SFX ===")]
    [SerializeField] private Button sfxButton;
    [SerializeField] private GameObject sfxOnUI;
    [SerializeField] private GameObject sfxOffUI;

    [Header("=== VIBRATION ===")]
    [SerializeField] private Button vibrationButton;
    [SerializeField] private GameObject vibrationOnUI;
    [SerializeField] private GameObject vibrationOffUI;

    private bool musicOn;
    private bool sfxOn;
    private bool vibrationOn;

    private void Start()
    {
        // ✅ GIỮ Y HỆT CLASS CŨ
        musicOn = PlayerPrefs.GetFloat("musicVolume", 1f) > 0.5f;
        sfxOn = PlayerPrefs.GetFloat("SFXVolume", 1f) > 0.5f;
        vibrationOn = PlayerPrefs.GetInt("vibrationOn", 1) == 1;

        ApplyMusic();
        ApplySFX();
        ApplyVibration();

        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSFX);
        vibrationButton.onClick.AddListener(ToggleVibration);
    }

    // ================= MUSIC =================
    private void ToggleMusic()
    {
        musicOn = !musicOn;
        ApplyMusic();
    }

    private void ApplyMusic()
    {
        // ✅ GIỮ LOGIC CŨ
        float volume = musicOn ? 1f : 0.0001f;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);

        // ✅ UI ON/OFF
        musicOnUI.SetActive(musicOn);
        musicOffUI.SetActive(!musicOn);
    }

    // ================= SFX =================
    private void ToggleSFX()
    {
        sfxOn = !sfxOn;
        ApplySFX();
    }

    private void ApplySFX()
    {
        // ✅ GIỮ LOGIC CŨ
        float volume = sfxOn ? 1f : 0.0001f;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);

        // ✅ UI ON/OFF
        sfxOnUI.SetActive(sfxOn);
        sfxOffUI.SetActive(!sfxOn);
    }

    // ================= VIBRATION =================
    private void ToggleVibration()
    {
        vibrationOn = !vibrationOn;
        ApplyVibration();
    }

    private void ApplyVibration()
    {
        PlayerPrefs.SetInt("vibrationOn", vibrationOn ? 1 : 0);

        vibrationOnUI.SetActive(vibrationOn);
        vibrationOffUI.SetActive(!vibrationOn);
    }

    // ================= STATIC CALL =================
    public static void Vibrate()
    {
        if (PlayerPrefs.GetInt("vibrationOn", 1) == 1)
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}