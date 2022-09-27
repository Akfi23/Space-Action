using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using UnityEngine;

namespace Akfi
{
    public class ShopScreen : UIScreen
    {
        public Dictionary<UpgradeType, UpgradeBarComponent> UpgradeBars = new Dictionary<UpgradeType, UpgradeBarComponent>();
        public Dictionary<UpgradeOwner, ShopPanelComponent> ShopScreens = new Dictionary<UpgradeOwner, ShopPanelComponent>();

        [SerializeField] private PlayerShopComponent playerShopPanel;
        [SerializeField] private ToolShopComponent toolShopPanel;

        public PlayerShopComponent PlayerShopPanel => playerShopPanel;
        public ToolShopComponent ToolShopPanel => toolShopPanel;

        public override void Subscribe()
        {
            ShopScreens = FindObjectsOfType<ShopPanelComponent>().ToDictionary(x => x.Owner);

            foreach (var shop in ShopScreens)
            {
                shop.Value.InitScreen();
            }
        }
    }
}
