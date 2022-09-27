using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    [Serializable]
    public class PlayerUpgradeData
    {
        public int Level;
        public UpgradeType Type;
        public float UpgradeValue;

        public PlayerUpgradeData(int level, UpgradeType type, float value)
        {
            Level = level;
            Type = type;
            UpgradeValue = value;
        }
    }
}
