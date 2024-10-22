using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "MeleeAttack", menuName = "EnemyLogic/AttackLogic/MeleeAttack")]

public class EnemyMeleeAttackSO : EnemyAttackSOBase
{
    [Header("Melee Attack Properties")]
    [SerializeField] private GameObject _meleePrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _knockback;

    [Header("State Properties")]
    [SerializeField] private float _attackDelay;

    private Transform _attackPoint;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.Attack)
        {
            //Play attack sound
            AudioManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);

            //Spawn attack object
            GameObject meleeAttack = Instantiate(_meleePrefab, _attackPoint.position, _enemy.Ring.rotation);
            meleeAttack.GetComponent<EnemyMelee>().Initialize(_damage, _knockback);

            //Set attack delay
        }
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _attackPoint = _enemy.Ring.GetChild(0);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
