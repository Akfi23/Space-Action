using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInitSystem : GameSystem
{
    public override void OnInit()
    {
        game.Enemies = FindObjectsOfType<EnemyComponent>().ToList();

        foreach (var enemy in game.Enemies)
        {
            enemy.Init();
            enemy.FSM.Init(game);
        }
    }
}
