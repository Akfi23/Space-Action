using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateUpdaterSystem : GameSystem
{
    public override void OnUpdate()
    {
        foreach (var enemy in game.Enemies)
        {
            enemy.FSM.Work();
        }
    }

    public override void OnStateExit()
    {
        foreach (var enemy in game.Enemies)
        {
            if(enemy.CurrentHealth>0)
                enemy.FSM.SetState(StateType.GoBack);
        }
    }
}
