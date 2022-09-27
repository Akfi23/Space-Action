using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeConfig/Upgrade", order = 51)]
    public class UpgradeConfig : ScriptableObject
    {
        public Sprite Icon;
        public int Price;
        public int UpgradeLevel;
        public float UpgradeValue;
        public UpgradeOwner Owner;
        public UpgradeType Type;
        public float PriceMultiplyer;
        public float UpgradeMultiplyer;
    }
}
