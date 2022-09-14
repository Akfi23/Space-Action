using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoBackState", menuName = "CharacterState/GoBackState", order = 51)]
public class GoBackState : CharacterState
{
    public override void OnStateEnter(EnemyComponent enemy)
    {
        enemy.Agent.SetDestination(enemy.BornPos);
        enemy.Animator.SetEnemyRun(true);
    }

    public override void OnStateExit(EnemyComponent enemy)
    {
    }

    public override void Work(EnemyComponent enemy)
    {
        if (Vector3.Distance(enemy.BornPos, enemy.transform.position) <= 1)
            enemy.FSM.SetState(StateType.Idle);
    }   
}
