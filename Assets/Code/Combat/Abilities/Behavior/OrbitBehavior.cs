using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The orbit behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Orbit", menuName = "Attack/Orbit")]
public class OrbitBehavior : Behavior
{
    [Header("Orbit Attributes")]
    [SerializeField] private GameObject _orbit;
    [SerializeField] private int _orbitCount;
    [SerializeField] private float _spreadAngle = 360f;

    [Header("Prefab Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _distance;

    private OrbitStruct _orbitStruct;

    public override void Initialize(AbilityBase abilityBase)
    {
        base.Initialize(abilityBase);
        _orbitStruct = new OrbitStruct(_damage, _knockback, _activationTime, _rotationSpeed, _distance);
    }

    public override void Activate(InputAction.CallbackContext context, Vector2 attackPosition, Quaternion rotation)
    {
        //if (context.started)
        //{
        //    GameObject newOrbit = Instantiate(_orbit, attackPosition, Quaternion.identity);
        //    newOrbit.GetComponent<Orbit>().SetOrbitStruct(_orbitStruct);
        //}
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {

            // Calculate the angle difference between each orbitball
            float angleStep = _spreadAngle / _orbitCount;
            float currentAngle = 0f;

            for (int i = 0; i < _orbitCount; i++)
            {
                // Spawn the orbitball at the player's position
            GameObject newOrbit = Instantiate(_orbit, attackPosition, Quaternion.identity);

                // Set the orbitball's attributes and its initial angle
                newOrbit.GetComponent<Orbit>().SetOrbitStruct(_orbitStruct);
                newOrbit.GetComponent<Orbit>().SetInitialAngle(currentAngle);

                // Increment the angle for the next orbitball
                currentAngle += angleStep;
            }
        
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
            case StatsEnum.DAMAGE:
                _orbitStruct._damage += (int)playerstats.GetStat(StatsEnum.DAMAGE);
                break;
            case StatsEnum.KNOCKBACK:
                _orbitStruct._knockback += playerstats.GetStat(StatsEnum.KNOCKBACK);
                break;
            case StatsEnum.ROTATION_SPEED:
                _orbitStruct._rotationSpeed += (int)playerstats.GetStat(StatsEnum.ROTATION_SPEED);
                break;
            case StatsEnum.ACTIVATION_TIME:
                _orbitStruct._activationTime += playerstats.GetStat(StatsEnum.ACTIVATION_TIME);
                break;
        }

    }
}
