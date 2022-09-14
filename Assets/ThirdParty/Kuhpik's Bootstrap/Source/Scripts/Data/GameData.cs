using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // public float LevelProgress;
        // public Enemy[] Enemies;

        public bool isPlayerMoving;

        public PlayerComponent Player;
        public CompanionComponent Companion;
        public FloatingJoystick Joystick;

        public bool isAttack;

        public List<GameObject> Bullets = new List<GameObject>();
        public List<EnemyComponent> Enemies = new List<EnemyComponent>();
    }
}