using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public enum SoundType
    {
        Attack,
        TowerDamaged,
        TowerDestroy,
        LevelUp,
        GameOver,
        LootGold,

    }

    public enum SoundCategory
    {
        Music,
        SFX
    }

    [System.Serializable]
    public class SoundData
    {
        public SoundType type;
        public SoundCategory category;
        public AudioClip clip;
        public bool loop;
    }
    [Header("Audio Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Sound Data")]
    public List<SoundData> sounds;

    private Dictionary<SoundType, AudioSource> sources = new();
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            var src = gameObject.AddComponent<AudioSource>();
            src.clip = s.clip;
            src.loop = s.loop;
            src.playOnAwake = false;

            src.outputAudioMixerGroup =
                s.category == SoundCategory.Music ? musicGroup : sfxGroup;

            sources[s.type] = src;
        }
    }
    public void Play(SoundType type)
    {
        if (!sources.ContainsKey(type)) return;

        var src = sources[type];
        if (!src.isPlaying)
            src.Play();
    }

    public void Stop(SoundType type)
    {
        if (!sources.ContainsKey(type)) return;
        sources[type].Stop();
    }

    public void PlayOneShot(SoundType type)
    {
        if (!sources.ContainsKey(type)) return;
        sources[type].PlayOneShot(sources[type].clip);
    }
}
//MusicManager.Instance.PlayOneShot(MusicManager.SoundType.Attack);
