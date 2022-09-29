using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kuhpik;
using Supyrb;
using Akfi;

public class LevelLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Clear();
        Application.targetFrameRate = 120;

        game.Rocket = FindObjectOfType<RocketComponent>();
        game.LandingPos = FindObjectOfType<RocketPosMarker>();
    }
}
