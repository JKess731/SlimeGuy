using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrontSmash", menuName = "EnemyLogic/AttackLogic/FrontSmash")]
public class EnemyAttackSmash : EnemyAttackSOBase
{
    [SerializeField] private GameObject smashTriggerPrefab;
    [SerializeField] private int frontSmashDamage;
    [SerializeField] private float frontSmashAttackDelay;
    [SerializeField] private Transform attackPoint;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        GameObject smash = Instantiate(smashTriggerPrefab, attackPoint.position, Quaternion.identity);
        _enemy.stateMachine.ChangeState(_enemy.chaseState);

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfAttack, _enemyTransform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDamaged)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfHurt, _enemyTransform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDeath)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfDeath, _enemyTransform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.GolemAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.GolemAttack, _enemyTransform.position);
        }

    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        attackPoint = _enemy.transform.GetChild(1).GetChild(0);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        _enemy.MoveEnemy(Vector2.zero);
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
