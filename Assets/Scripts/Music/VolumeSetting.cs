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

        musicOn = PlayerPrefs.GetInt("musicOn", 1) == 1;
        sfxOn = PlayerPrefs.GetInt("sfxOn", 1) == 1;
        vibrationOn = PlayerPrefs.GetInt("vibrationOn", 1) == 1;

        ApplyMusic();
        ApplySFX();
        ApplyVibration();

        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSFX);
        vibrationButton.onClick.AddListener(ToggleVibration);

       
    }

   
    private void ToggleMusic()
    {
        musicOn = !musicOn;
        ApplyMusic();
    }

    private void ApplyMusic()
    {
        float volume = musicOn ? 1f : 0.0001f; 
        float dB = Mathf.Log10(volume) * 20;

        myMixer.SetFloat("music", dB);

        PlayerPrefs.SetInt("musicOn", musicOn ? 1 : 0);


        musicOnUI.SetActive(musicOn);
        musicOffUI.SetActive(!musicOn);
    }

    private void ToggleSFX()
    {
        sfxOn = !sfxOn;
        ApplySFX();
    }

    private void ApplySFX()
    {
        float volume = sfxOn ? 1f : 0.01f; 
        float dB = Mathf.Log10(volume) * 20;

        myMixer.SetFloat("SFX", dB);

        PlayerPrefs.SetInt("sfxOn", sfxOn ? 1 : 0);

        sfxOnUI.SetActive(sfxOn);
        sfxOffUI.SetActive(!sfxOn);
    }

   
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