using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents instance;

    [field:Header("Player")]
    [field: SerializeField] public EventReference playerAttack { get; private set; }
    [field: SerializeField] public EventReference playerDash { get; private set; }
    [field:SerializeField] public EventReference playerHurt { get; private set; }
    [field:SerializeField] public EventReference playerDeath { get; private set; }
    [field: SerializeField] public EventReference playerPickUp { get; private set; }
    [field: SerializeField] public EventReference NikoSong { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Debug.LogWarning("More than one instance of FmodEvents found!");
            Destroy(gameObject);
        }
    }
}
