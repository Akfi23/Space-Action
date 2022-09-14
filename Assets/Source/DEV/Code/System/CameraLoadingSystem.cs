using Cinemachine;
using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadingSystem : GameSystem
{
    [SerializeField] [Tag] private string gameCameraTag;
    [SerializeField] [Tag] private string combatCameraTag;

    public override void OnInit()
    {
        cameraController.GameCamera = GameObject.FindGameObjectWithTag(gameCameraTag).GetComponent<CinemachineVirtualCamera>();
        cameraController.CombatCamera = GameObject.FindGameObjectWithTag(combatCameraTag).GetComponent<CinemachineVirtualCamera>();
    }
}
