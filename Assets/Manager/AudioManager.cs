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
    public EventInstance IValleyTheme;

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

    public static void PlayOneShot(EventReference eventReference, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(eventReference, worldPos);
    }

    //Can be used to remove Play Niko and Stop Niko && Play Jeopardy and Stop Jeopardy
    public static EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);
        return instance;
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
    public void PlayIValley()
    {
        IValleyTheme = RuntimeManager.CreateInstance(FmodEvents.instance.IValleyTheme);
        IValleyTheme.start();
    }
    public void StopIValley()
    {
        IValleyTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //The curse of Jeopardy
    //Remove these two methods when possible
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
