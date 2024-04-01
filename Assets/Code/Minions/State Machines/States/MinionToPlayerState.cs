using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionToPlayerState : MinionState
{
    private List<GameObject> _enemyList;
    private GameObject _target;
    private GameObject _player;
    private Vector2 _direction;

    public MinionToPlayerState(Minion minion, MinionStateMachine stateMachine) : base(minion, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Minion.AnimationTriggerType animType)
    {
        base.AnimationTriggerEvent(animType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("MINION: Enter to Player state");

        _player = GameObject.FindWithTag("player");

        _enemyList = EnemiesInLevel.instance.GetEnemies(EnemiesInLevel.instance.currentRoom);
        _target = _player;
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("MINION: Exit to Player state");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _enemyList = EnemiesInLevel.instance.GetEnemies(EnemiesInLevel.instance.currentRoom);

        if (IsEnemyInLookDistance((minion.RemoveNullEnemies(_enemyList, minion.GetNullEnemies(_enemyList)))))
        {
            stateMachine.ChangeState(minion.toEnemyState);

        }
        else
        {
            
            if (Vector3.Distance(minion.transform.position, _player.transform.position) > 1.5f)
            {
                minion.MoveMinion(_player.transform.position);
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private bool IsEnemyInLookDistance(List<GameObject> enemyList)
    {
        bool output = false;

        foreach (GameObject enemy in enemyList)
        {
            if (Vector3.Distance(minion.transform.position, enemy.transform.position) <= minion.lookDistance)
            {
                output = true;
                break;
            }
        }

        return output;
    }
}
