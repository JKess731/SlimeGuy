using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
   
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound music = Array.Find(musicSounds, x => x.name == name);
        
        if(music == null)
        {
            UnityEngine.Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = music.audioClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(sfxSounds, x => x.name == name);

        if (sfx == null)
        {
            UnityEngine.Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sfx.audioClip);
        }
    }
}
