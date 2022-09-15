using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;
using Cinemachine;
using System;

namespace Kuhpik
{
    [Serializable]
    public class CameraController : MonoBehaviour
    {
        public CinemachineVirtualCamera GameCamera { get; set; }
        public CinemachineVirtualCamera CombatCamera { get; set; }

        public void SetCombatCameraActive()
        {
            if (GameCamera.gameObject.activeSelf)
                GameCamera.gameObject.SetActive(false);
        }

        public void SetGameCameraActive()
        {
            if (!GameCamera.gameObject.activeSelf)
                GameCamera.gameObject.SetActive(true);
        }
    }
}
