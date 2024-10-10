using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SmashAttack", menuName = "EnemyLogic/AttackLogic/SmashAttack")]

public class EnemySmashAttack : EnemyAttackSOBase
{
    [SerializeField] private GameObject slashTriggerPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int frontSmashDamage;
    [SerializeField] private float frontSmashAttackDelay;
    public GameObject ring;
    private float timer;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.GolemAttack)
        {
           //RuntimeManager.PlayOneShot(_enemy.GolemAttack, _enemy.transform.position);
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
        if (timer > frontSmashAttackDelay)
        {
            timer = 0f;
            GameObject slash = Instantiate(slashTriggerPrefab, attackPoint.position, ring.transform.rotation);
            _enemy.stateMachine.ChangeState(_enemy.chaseState);
        }
        if (timer < frontSmashAttackDelay)
        {
            Vector3 rotation = _playerTransform.position - ring.transform.position;
            float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);
        }
        timer += Time.deltaTime;
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
