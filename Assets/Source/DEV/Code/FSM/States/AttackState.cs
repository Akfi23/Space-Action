using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "CharacterState/AttackState", order = 51)]
public class AttackState : CharacterState
{
    public override void OnStateEnter(EnemyComponent enemy)
    {
        enemy.Agent.ResetPath();
        enemy.Animator.SetEnemyRun(false);
        enemy.Animator.SetEnemyAttack(true);

        Debug.Log(this.name);
    }

    public override void OnStateExit(EnemyComponent enemy)
    {
    }

    public override void Work(EnemyComponent enemy)
    {
        Debug.Log(Vector3.Distance(gamedata.Player.transform.position, enemy.transform.position));

        if (Vector3.Distance(gamedata.Player.transform.position, enemy.transform.position) > 3)
            enemy.FSM.SetState(StateType.Chase);

        if (Vector3.Distance(gamedata.Player.transform.position, enemy.transform.position) > 5)
            enemy.FSM.SetState(StateType.GoBack);
    }
}
