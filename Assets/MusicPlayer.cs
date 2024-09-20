using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
       AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfAttack, _transform.position); 
    }
}
