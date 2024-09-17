using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.NikoSong, transform.position);
    }
}
