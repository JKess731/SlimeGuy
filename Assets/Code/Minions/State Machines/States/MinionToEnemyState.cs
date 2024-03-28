using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MinionToEnemyState : MinionState
{
    private List<GameObject> _enemyList;
    private GameObject _target;
    private Vector3 _direction;

    public MinionToEnemyState(Minion minion, MinionStateMachine stateMachine) : base(minion, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Minion.AnimationTriggerType animType)
    {
        base.AnimationTriggerEvent(animType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("MINION: Enter to Enemy state");

        _enemyList = EnemiesInLevel.instance.GetEnemies(EnemiesInLevel.instance.currentRoom);
        _target = minion.FindNearestEnemy(minion.RemoveNullEnemies(_enemyList ,minion.GetNullEnemies(_enemyList)));
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("MINION: Exit to Enemy state");
        stateMachine.target = _target;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_target == null && _enemyList.Count > 0)
        {
            _target = minion.FindNearestEnemy(minion.RemoveNullEnemies(_enemyList, minion.GetNullEnemies(_enemyList)));
        }
        else if (_target == null && _enemyList.Count <= 0)
        {
            stateMachine.ChangeState(minion.toPlayerState);
        }

        if (_target != null)
        {
            if (Vector3.Distance(minion.transform.position, _target.transform.position) > 3f)
            {
                minion.MoveMinion(_target.transform.position);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
