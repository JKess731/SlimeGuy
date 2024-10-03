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
    public GameObject ring;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == EnemyBase.AnimationTriggerType.DwarfAttack)
        {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.DwarfAttack, _transform.position);
            //tweak Edison
            Instantiate(slashTriggerPrefab, attackPoint.position, attackPoint.rotation);
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
