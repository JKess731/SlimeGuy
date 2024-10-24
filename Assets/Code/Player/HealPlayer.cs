using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    [SerializeField] StatsSO _stats;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _stats.SetStat(Enum_Stats.HEALTH, _stats.GetStat(Enum_Stats.MAXHEALTH));
        }
    }
}
