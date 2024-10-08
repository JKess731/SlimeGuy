using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Dash", menuName = "Behavior/Dash")]
public class DashBehavior : AbilitySOBase
{

    [Header("Prefab Attributes")]
    [SerializeField] private GameObject _dashPrefab;
    [SerializeField] private float _dashSpeed;

    public override void Initialize()
    {
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        //GameObject dash = 
        //newWhip.GetComponent<Whip>().SetWhipStruct(_whipStruct);

        AbilityState = AbilityState.PERFORMING;
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.CANCELING;
    }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.FINISHED;
        onBehaviorFinished?.Invoke();
    }

    public override void Upgrade(StatsSO playerstats, StatsEnum stat)
    {
        switch (stat)
        {
            case StatsEnum.ACTIVATION_TIME:
                _activationTime += playerstats.GetStat(StatsEnum.ACTIVATION_TIME);
                break;
        }

    }
}
