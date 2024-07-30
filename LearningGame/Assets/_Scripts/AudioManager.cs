using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio sources
    [SerializeField] private AudioSource musicSource, effectsSource;

    // singleton pattern
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        if (effectsSource != null)
        {
            effectsSource.pitch = 1f;
            effectsSource.PlayOneShot(clip);
        }
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

}