using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "CharacterState/IdleState", order = 51)]
public class IdleState : CharacterState
{
    public override void OnStateEnter(EnemyComponent enemy)
    {
        enemy.Animator.SetEnemyAttack(false);
        enemy.Animator.SetEnemyRun(false);
        enemy.Agent.ResetPath();
    }

    public override void OnStateExit(EnemyComponent enemy)
    {
    }

    public override void Work(EnemyComponent enemy)
    {
    }
}
