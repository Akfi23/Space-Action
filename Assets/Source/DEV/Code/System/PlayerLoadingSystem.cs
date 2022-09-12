using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        game.Player = FindObjectOfType<PlayerComponent>();
        game.Companion = FindObjectOfType<CompanionComponent>();

        game.Player.Init();
        game.Companion.Init();
    }
}
