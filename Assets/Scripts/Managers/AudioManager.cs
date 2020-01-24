using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Toggle mute;
    [SerializeField] private Toggle music;
    [Space(10)]
    [SerializeField] private Scrollbar musicSlider;
    [SerializeField] private TextMeshProUGUI textSliderMusicVolume;
    [Space(10)]
    [SerializeField] private Scrollbar backgroundSlider;
    [SerializeField] private TextMeshProUGUI textSliderBackgroundVolume;
    [Space(10)]
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        //SetupSingleton();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    // ALTERNATE SINGLETON
    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Play("Theme");
        Play("Music");
    }

    private void Update()
    {
        MenuSettings("Theme");
        MenuSettings("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.mute = s.mute;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void MenuSettings(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (name == "Theme")
        {
            if (SceneManager.GetActiveScene().buildIndex >= 1)
            {
                return;
            }
            else
            {
                s.volume = backgroundSlider.value;
            }
            textSliderBackgroundVolume.text = s.volume.ToString();
        }

        if (name == "Music")
        {
            if (SceneManager.GetActiveScene().buildIndex >= 1)
            {
                return;
            }
            else
            {
                s.volume = musicSlider.value;
            }
            textSliderMusicVolume.text = s.volume.ToString();

            s.source.mute = s.mute;
            if (!music.isOn) s.mute = true;
        }

        s.source.volume = s.volume;
        s.source.mute = s.mute;
        s.mute = mute.isOn;
    }
}