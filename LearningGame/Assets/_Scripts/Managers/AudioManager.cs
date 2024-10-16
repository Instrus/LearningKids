using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio sources
    [SerializeField] private AudioSource musicSource, effectsSource;
    PlayerData playerData;

    // singleton pattern
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        effectsSource.volume = playerData.GetEffectsVolume();
        musicSource.volume = playerData.GetMusicVolume();
    }

    public void PlayClip(AudioClip clip)
    {
        ChangePitch(1);

        if (effectsSource != null)
            effectsSource.PlayOneShot(clip);
    }

    public void ChangePitch(float pitch)
    {
        effectsSource.pitch = pitch;
    }

    public void PlaySoundRandomPitch(AudioClip clip)
    {
        if (effectsSource != null)
        {
            effectsSource.pitch = Random.Range(0.9f, 1.1f);
            effectsSource.PlayOneShot(clip);
        }
    }

    public void ChangeMusic(AudioClip music)
    {
        musicSource.clip = music;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void ChangeEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }

}