using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Akfi;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        // Example (I use public fields for data, but u free to use properties\methods etc)
        // [BoxGroup("level")] public int level;
        // [BoxGroup("currency")] public int money;

        public int Money = 5000;
        public Dictionary<UpgradeType, PlayerUpgradeData> PlayerUpgradeDatas = new Dictionary<UpgradeType, PlayerUpgradeData>();
    }
}