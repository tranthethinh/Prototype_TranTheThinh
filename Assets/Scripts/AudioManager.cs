using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource shootSoundSource;
    public AudioSource walkSoundSource;
    public AudioSource backgroundSoundSource;
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    public float soundVolume = 0.5f;
    // Ensure only one instance of AudioManager exists
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Play shoot sound
    public void PlayShootSound()
    {
        if (shootSoundSource != null)
        {
            shootSoundSource.Play();
        }
        else
        {
            Debug.LogWarning("Shoot sound source not assigned!");
        }
    }

    // Play walk sound
    public void PlayWalkSound()
    {
        if (walkSoundSource != null)
        {
            if (!walkSoundSource.isPlaying)
            {
                walkSoundSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Walk sound source not assigned!");
        }
    }

    // Stop walk sound
    public void StopWalkSound()
    {
        if (walkSoundSource != null)
        {
            walkSoundSource.Stop();
            //Debug.Log("Stop ok");
        }
        else
        {
            Debug.LogWarning("Walk sound source not assigned!");
        }
    }

    // Play background sound
    public void PlayBackgroundSound()
    {
        if (backgroundSoundSource != null)
        {
            backgroundSoundSource.Play();
        }
        else
        {
            Debug.LogWarning("Background sound source not assigned!");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        backgroundSoundSource.volume = musicVolume;
    }
    public void SetSoundVolume(float volume)
    {
        soundVolume = Mathf.Clamp01(volume);
        shootSoundSource.volume = soundVolume;
        walkSoundSource.volume = soundVolume;
    }
}
