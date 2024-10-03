using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents instance;

    [field:Header("Music")]
    [field: SerializeField] public EventReference NikoSong { get; private set; }
    [field: SerializeField] public EventReference Jepardy { get; private set; }


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
    [field: SerializeField] public EventReference DwarfStep{ get; private set; }


    [field: Header("Golem")]
    [field: SerializeField] public EventReference GolemAttack { get; private set; }

    [field: SerializeField] public EventReference GolemDamage { get; private set; }

    [field: SerializeField] public EventReference GolemDeath { get; private set; }

    [field: SerializeField] public EventReference GolemStep{ get; private set; }

    [field: Header("Wizard")]
    [field: SerializeField] public EventReference WizardCast { get; private set; }

    [field: SerializeField] public EventReference WizardDamage { get; private set; }

    [field: SerializeField] public EventReference WizardDeath { get; private set; }

    [field: SerializeField] public EventReference WizardTeleport { get; private set; }



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
