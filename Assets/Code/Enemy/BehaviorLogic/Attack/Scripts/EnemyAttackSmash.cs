using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrontSmash", menuName = "EnemyLogic/AttackLogic/FrontSmash")]
public class EnemyAttackSmash : EnemyAttackSOBase
{
    [SerializeField] private GameObject smashTriggerPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int frontSmashDamage;
    [SerializeField] private float frontSmashAttackDelay;
    public GameObject ring;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        GameObject smash = Instantiate(smashTriggerPrefab, attackPoint.position, Quaternion.identity);
        _enemy.stateMachine.ChangeState(_enemy.chaseState);

        Vector3 rotation = _playerTransform.position - ring.transform.position;
        float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfAttack, _transform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDamaged)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfHurt, _transform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfDeath)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfDeath, _transform.position);
        }

        if (triggerType == EnemyBase.AnimationTriggerType.GolemAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.GolemAttack, _transform.position);
            rotation = new Vector3(0, 0, 0);
        }

    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        attackPoint = _enemy.transform.GetChild(1).GetChild(0);
        ring = _enemy.transform.GetChild(1).gameObject;

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
