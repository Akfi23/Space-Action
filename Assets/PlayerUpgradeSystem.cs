using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kuhpik;

namespace Akfi
{
    public class PlayerUpgradeSystem : GameSystemWithScreen<ShopScreen>
    {
        [SerializeField] private UpgradeBarComponent barPrefab;
        private GameScreen gameScreen;
        private Dictionary<UpgradeType, UpgradeConfig> playerUpgradeConfigs = new Dictionary<UpgradeType, UpgradeConfig>();

        public override void OnInit()
        {
            InitUpgrades();
            gameScreen = UIManager.GetUIScreen<GameScreen>();
        }

        private void InitUpgrades()
        {
            var configs = Resources.LoadAll<UpgradeConfig>("UpgradeConfigs");

            if (player.PlayerUpgradeDatas.Count <= 0)
            {
                foreach (var config in configs)
                {
                    UpgradeBarComponent newBar = Instantiate(barPrefab, screen.PlayerShopPanel.ContentWindow);
                    screen.UpgradeBars.Add(config.Type, newBar);
                    playerUpgradeConfigs.Add(config.Type, config);
                    player.PlayerUpgradeDatas.Add(config.Type, new PlayerUpgradeData(config.UpgradeLevel, config.Type, config.UpgradeValue));

                    newBar.Init(config.Icon, config.Type.ToString(), config.Price, player.Money);
                    newBar.Button.onClick.AddListener(() => Upgrade(config.Type));
                }
            }
            else
            {
                foreach (var config in configs)
                {
                    UpgradeBarComponent newBar = Instantiate(barPrefab, screen.PlayerShopPanel.ContentWindow);
                    screen.UpgradeBars.Add(config.Type, newBar);
                    playerUpgradeConfigs.Add(config.Type, config);

                    var price = (player.PlayerUpgradeDatas[config.Type].Level + 1) * playerUpgradeConfigs[config.Type].Price;
                    newBar.Init(config.Icon, config.Type.ToString(), price, player.Money);
                    newBar.Button.onClick.AddListener(() => Upgrade(config.Type));
                }
            }
        }

        private void Upgrade(UpgradeType type)
        {
            var price = (player.PlayerUpgradeDatas[type].Level + 1) * playerUpgradeConfigs[type].Price;
            var value = (player.PlayerUpgradeDatas[type].Level + 1) * playerUpgradeConfigs[type].UpgradeMultiplyer;

            if (player.Money < price) return;

            player.Money -= price;
            gameScreen.UpdateMoney(player.Money);

            player.PlayerUpgradeDatas[type].Level++;
            player.PlayerUpgradeDatas[type].UpgradeValue = value;

            UpdateAllBars();
            ApplyUpgrade(type);
        }

        private void UpdateAllBars()
        {
            foreach (var config in playerUpgradeConfigs)
            {
                var price = (player.PlayerUpgradeDatas[config.Value.Type].Level + 1) * playerUpgradeConfigs[config.Value.Type].Price;
                screen.UpgradeBars[config.Value.Type].UpdateStats(price, player.Money);
            }
        }

        private void ApplyUpgrade(UpgradeType type)
        {
            if (type == UpgradeType.AttackRange)
            {
                game.Player.UpdateAttackRange(config.PlayerConfig.AttackRadiusBase + player.PlayerUpgradeDatas[UpgradeType.AttackRange].UpgradeValue);
            }
            else if (type == UpgradeType.Health)
            {
                game.Player.UpdateHealthValue(player.PlayerUpgradeDatas[type].UpgradeValue);
            }
        }
    }
}

