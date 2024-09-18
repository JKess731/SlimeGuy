using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents instance;

    [field:Header("Music")]
    [field: SerializeField] public EventReference NikoSong { get; private set; }


    [field:Header("Player")]
    [field: SerializeField] public EventReference playerAttack { get; private set; }
    [field: SerializeField] public EventReference playerDash { get; private set; }
    [field:SerializeField] public EventReference playerHurt { get; private set; }
    [field:SerializeField] public EventReference playerDeath { get; private set; }
    [field: SerializeField] public EventReference playerPickUp { get; private set; }


    [field:Header("Dwarf")]
    [field:SerializeField] public EventReference DwarfAttack { get; private set; }
    [field:SerializeField] public EventReference DwarfHurt { get; private set; }
    [field:SerializeField] public EventReference DwarfDeath { get; private set; }

    [field: SerializeField] public EventReference DwarfStep1 { get; private set; }
    [field: SerializeField] public EventReference DwarfStep2 { get; private set; }
    [field: SerializeField] public EventReference DwarfStep3 { get; private set; }

    [field: Header("Golem")]
    [field: SerializeField] public EventReference GolemAttack1 { get; private set; }
    [field: SerializeField] public EventReference GolemAttack2 { get; private set; }
    [field: SerializeField] public EventReference GolemAttack3 { get; private set; }

    [field: SerializeField] public EventReference GolemDamage1 { get; private set; }
    [field: SerializeField] public EventReference GolemDamage2 { get; private set; }
    [field: SerializeField] public EventReference GolemDamage3 { get; private set; }
    [field: SerializeField] public EventReference GolemDamage4 { get; private set; }
    [field: SerializeField] public EventReference GolemDamage5 { get; private set; }
    [field: SerializeField] public EventReference GolemDamage6 { get; private set; }

    [field: SerializeField] public EventReference GolemDeath { get; private set; }

    [field: SerializeField] public EventReference GolemStep1 { get; private set; }
    [field: SerializeField] public EventReference GolemStep2 { get; private set; }
    [field: SerializeField] public EventReference GolemStep3 { get; private set; }


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
