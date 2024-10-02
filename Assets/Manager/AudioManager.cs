using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

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
        OnGameStart();
    }

    public void OnGameStart()
    {
        PlayOneShot(FmodEvents.instance.NikoSong, transform.position);
    }

    public void PlayOneShot(EventReference eventReference, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(eventReference, worldPos);
    }
}
