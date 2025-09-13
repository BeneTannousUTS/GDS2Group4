using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, soundEffectSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    public void PlaySound(AudioClip soundClip)
    {
        soundEffectSource.PlayOneShot(soundClip);
    }
    public void ToggleMusic()
    {
        if (musicSource.isPlaying) musicSource.Pause();
        else musicSource.Play();
    }
}
