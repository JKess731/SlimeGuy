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
            _stats.SetStat(StatsEnum.HEALTH, _stats.GetStat(StatsEnum.MAXHEALTH));
        }
    }
}
