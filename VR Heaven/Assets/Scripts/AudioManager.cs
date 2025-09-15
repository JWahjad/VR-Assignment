using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic, fireworksSfx;
    [SerializeField] private float musicVolume = 0.5f;
    [SerializeField] private float fireworksVolume = 1f;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource fireworksSource;

    private void Awake()
    {
        if (musicSource == null)
            Debug.LogWarning("Music AudioSource not assigned.");
        if (fireworksSource == null)
            Debug.LogWarning("Fireworks AudioSource not assigned.");

        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.volume = musicVolume;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music AudioSource or clip not assigned.");
        }
    }

    public void PlayFireworks()
    {
        if (fireworksSource != null && fireworksSfx != null)
        {
            fireworksSource.clip = fireworksSfx;
            fireworksSource.volume = fireworksVolume;
            fireworksSource.loop = true;
            fireworksSource.Play();
        }
        else
        {
            Debug.LogWarning("Fireworks AudioSource or clip not assigned.");
        }
    }
}