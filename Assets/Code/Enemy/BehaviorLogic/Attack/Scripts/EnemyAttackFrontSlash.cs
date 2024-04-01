using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "FrontSlash", menuName = "EnemyLogic/AttackLogic/FrontSlash")]

public class EnemyAttackFrontSlash : EnemyAttackSOBase
{
    [SerializeField] private GameObject slashTriggerPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int frontSlashDamage;
    [SerializeField] private float frontSlashAttackDelay;
    private float timer;
    private float exitTimer;


    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        attackPoint = enemy.transform.GetChild(2).GetChild(0);
        
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.MoveEnemy(Vector2.zero);
        if (timer > frontSlashAttackDelay)
        {
            timer = 0f;
            GameObject slash = GameObject.Instantiate(slashTriggerPrefab, attackPoint.position, Quaternion.identity);
            enemy.stateMachine.ChangeState(enemy.chaseState);
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
