using FMODUnity;
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
    [SerializeField] private GameObject ring;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        //Vector3 rotation = _playerTransform.position - ring.transform.position;
        //float slashRotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //ring.transform.rotation = Quaternion.Euler(0, 0, slashRotZ);

        if (triggerType == EnemyBase.AnimationTriggerType.ATTACK)
        {
            if (_enemy.attackSoundEffects.Count > 0)
            {
                RuntimeManager.PlayOneShot(_enemy.attackSoundEffects[0], _enemy.transform.position);
            }
            GameObject smash = Instantiate(smashTriggerPrefab, attackPoint.position, Quaternion.identity);
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
