using Cinemachine;
using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoadingSystem : GameSystem
{
    [SerializeField][Tag] private string gameCameraTag;
    [SerializeField][Tag] private string combatCameraTag;
    [SerializeField][Tag] private string rocketCameraTag;


    public override void OnInit()
    {
        cameraController.GameCamera = GameObject.FindGameObjectWithTag(gameCameraTag).GetComponent<CinemachineVirtualCamera>();
        cameraController.CombatCamera = GameObject.FindGameObjectWithTag(combatCameraTag).GetComponent<CinemachineVirtualCamera>();
        cameraController.RocketCamera = GameObject.FindGameObjectWithTag(rocketCameraTag).GetComponent<CinemachineVirtualCamera>();
    }
}
