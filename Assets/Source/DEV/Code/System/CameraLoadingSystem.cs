using Cinemachine;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        game.GameCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }
}
