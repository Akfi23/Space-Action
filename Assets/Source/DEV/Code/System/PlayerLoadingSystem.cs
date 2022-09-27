using Akfi;
using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadingSystem : GameSystemWithScreen<GameScreen>
{
    public override void OnInit()
    {
        game.Player = FindObjectOfType<PlayerComponent>();
        game.Companion = FindObjectOfType<CompanionComponent>();

        screen.UpdateMoney(player.Money);

        game.Player.Init();
        game.Player.UpdateAttackRange(config.PlayerConfig.AttackRadiusBase);
        game.Companion.Init();

        UpgradeStatsByData();
    }

    private void UpgradeStatsByData()
    {
        if (player.PlayerUpgradeDatas.Count == 0) return;

        game.Player.UpdateAttackRange(config.PlayerConfig.ArmorBase + player.PlayerUpgradeDatas[UpgradeType.AttackRange].UpgradeValue);
        game.Player.UpdateHealthValue(config.PlayerConfig.ArmorBase + player.PlayerUpgradeDatas[UpgradeType.Health].UpgradeValue);
    }
}
