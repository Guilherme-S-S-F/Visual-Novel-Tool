using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;

    public void PlayMusic(AudioClip clip)
    {        
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void SoundMusic(AudioClip clip)
    {
        soundSource.Stop();
        soundSource.clip = clip;
        soundSource.Play();
    }

    private void Start()
    {
        musicSource.loop = true;
        soundSource.loop = true;
    }
}
