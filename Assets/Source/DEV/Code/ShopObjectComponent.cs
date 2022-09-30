using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    public class ShopObjectComponent : MonoBehaviour
    {
        [SerializeField] private UpgradeOwner owner;
        public UpgradeOwner Owner => owner;
    }
}