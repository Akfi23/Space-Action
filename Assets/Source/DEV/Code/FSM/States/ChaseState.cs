using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "CharacterState/ChaseState", order = 51)]
public class ChaseState : CharacterState
{
    public override void OnStateEnter(EnemyComponent enemy)
    {
        enemy.Animator.SetEnemyAttack(false);
        enemy.Animator.SetEnemyRun(true);

        Debug.Log(this.name);
    }

    public override void OnStateExit(EnemyComponent enemy)
    {
    }

    public override void Work(EnemyComponent enemy)
    {
        enemy.Agent.SetDestination(gamedata.Player.transform.position);

        if (Vector3.Distance(gamedata.Player.transform.position, enemy.transform.position) < 2)
            enemy.FSM.SetState(StateType.Attack);

        if (Vector3.Distance(gamedata.Player.transform.position, enemy.transform.position) > 8)
            enemy.FSM.SetState(StateType.GoBack);
    }
}
