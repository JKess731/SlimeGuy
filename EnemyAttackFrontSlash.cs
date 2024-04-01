using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "FronSlash", menuName = "EnemyLogic/AttackLogic/FrontSlash")]

public class EnemyAttackFrontSlash : EnemyAttackSOBase
{
    [SerializeField] Collider2D frontSlashPoint;
    [SerializeField] private int frontSlashDamage;
    [SerializeField] private float frontSlashAttackDelay;
    [SerializeField] public GameObject directionRing;
    private bool attacking = false;
    private float timer;
    private float exitTimer;


    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        directionRing = enemy.getchild(2);
        Debug.Log("EnterSlash");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.MoveEnemy(Vector2.zero);
        if (attacking == false)
        {
            Vector3 rotation = playerTransform.transform.position - directionRing.transform.position;
            float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            directionRing.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);
        }
        else
        {
            if(timer > frontSlashAttackDelay)
            {
                timer = 0f;
                playerTransform.GetComponentInParent<PlayerHealth>().Damage(slashDamage);
                attacking = false;
                enemy.stateMachine.ChangeState(enemy.chaseState);
            }
            timer += Time.deltaTime;

        }
        
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            attacking = true;
        }
    }

    


}
