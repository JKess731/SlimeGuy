using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

//Edison Li
//Code from: https://www.youtube.com/watch?v=rdX7nhH6jdM
//By: Rehope Games

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
   
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    /// <summary>
    /// Creates singelton and make sure it always loaded
    /// </summary>
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

    /// <summary>
    /// Plays music give audio name
    /// </summary>
    /// <param name="name"></param>
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

    /// <summary>
    /// Plays sfx given audio name
    /// </summary>
    /// <param name="name"></param>
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
