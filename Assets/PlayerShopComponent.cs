using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    public class PlayerShopComponent : ShopPanelComponent
    {
        [SerializeField] private Transform contentWindow;
        public Transform ContentWindow => contentWindow;
    }
}
