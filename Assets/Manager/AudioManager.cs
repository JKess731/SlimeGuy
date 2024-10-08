using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public EventInstance niko;
    public EventInstance jeopardy;
    public EventInstance menuMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("AudioManager instance already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(EventReference eventReference, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(eventReference, worldPos);
    }

    public void PlayNiko()
    {
        niko = RuntimeManager.CreateInstance(FmodEvents.instance.NikoSong);
        niko.start();
    }
    public void StopNiko()
    {
        niko.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayJeopardy()
    {
        //The curse of Jeopardy
        jeopardy = RuntimeManager.CreateInstance(FmodEvents.instance.Jepardy);
        jeopardy.start();
    }

    public void StopJeopardy()
    {
        jeopardy.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void PlayMenuMusic()
    {
        menuMusic = RuntimeManager.CreateInstance(FmodEvents.instance.MenuMusic);
        menuMusic.start();
    }

    public void StopMenuMusic()
    {
        menuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
