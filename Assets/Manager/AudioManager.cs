using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public EventInstance jeopardy;

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
    private void Start()
    {
        PlayJeopardy();
    }

    public void PlayOneShot(EventReference eventReference, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(eventReference, worldPos);
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
}
