using Kuhpik;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class CompanionBehaviourSystem : GameSystem
{
    private Vector3 lerpedDirection;
    private Transform target;
    private List<EnemyComponent> enemies = new List<EnemyComponent>();
    public override void OnInit()
    {
        Signals.Get<OnTriggerCollide>().AddListener(ManageEnemyList);
        Signals.Get<OnEnemyDie>().AddListener(FindNextTargetAfterKill);

        target = game.Player.transform;
    }

    public override void OnUpdate()
    {
        if (!game.Companion.Agent.enabled) return;


        if (game.isPlayerMoving)
        {
            game.Companion.Agent.SetDestination(target.position);
        }

        Quaternion lookRotation = Quaternion.LookRotation(target.position - game.Companion.transform.position);

        game.Companion.transform.rotation = lookRotation;
    }

    private void ManageEnemyList(Transform enemy, bool status)
    {
        if (!enemy.TryGetComponent(out EnemyComponent enemyComponent)) return;

        if (status)
        {
            AddEnemyToList(enemyComponent);
        }
        else
        {
            RemoveEnemyFromList(enemyComponent);
        }

        TryAttackEnemy();
    }

    private void AddEnemyToList(EnemyComponent enemyComponent)
    {
        enemies.Add(enemyComponent);
    }

    private void RemoveEnemyFromList(EnemyComponent enemyComponent)
    {
        enemies.Remove(enemyComponent);
    }

    private void TryAttackEnemy()
    {
        if (enemies.Count > 0)
        {
            if (enemies[0].CurrentHealth > 0)
            {
                target = enemies[0].transform;
            }
        }
        else
        {
            target = game.Player.transform;
        }
    }

    private void FindNextTargetAfterKill(EnemyComponent enemy)
    {
        RemoveEnemyFromList(enemy);
        TryAttackEnemy();
    }
}
