using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitSystem : GameSystem
{
    public override void OnInit()
    {
        var enemyList = FindObjectsOfType<EnemyComponent>();

        foreach (var enemy in enemyList)
        {
            enemy.Init();
        }
    }
}
