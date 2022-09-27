using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        game.Joystick = FindObjectOfType<FloatingJoystick>(true);
    }
}
