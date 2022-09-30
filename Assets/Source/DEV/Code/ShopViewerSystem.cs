using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using Supyrb;
using UnityEngine;

namespace Akfi
{
    public class ShopViewerSystem : GameSystemWithScreen<ShopScreen>
    {
        public override void OnInit()
        {
            Signals.Get<OnTriggerCollide>().AddListener(OpenShopScreen);
        }

        private void OpenShopScreen(Transform transform, bool status)
        {
            if (!transform.TryGetComponent(out ShopObjectComponent shop)) return;

            screen.ShopScreens[shop.Owner].ToggleScreen(status);
        }
    }
}
