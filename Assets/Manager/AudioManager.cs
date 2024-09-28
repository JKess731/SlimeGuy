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

    public void OnGameStart()
    {
        PlayOneShot(FmodEvents.instance.NikoSong, Vector3.zero);
    }

    public void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBase.AnimationTriggerType.PlayNikoSong)
        {
            PlayOneShot(FmodEvents.instance.NikoSong, Vector3.zero);
        }
    }

    public void PlayOneShot(EventReference eventReference, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(eventReference, worldPos);
    }
}
