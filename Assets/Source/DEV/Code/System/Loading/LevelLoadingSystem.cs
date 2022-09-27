using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kuhpik;
using Supyrb;

public class LevelLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Clear();
        Application.targetFrameRate = 120;
    }
}
