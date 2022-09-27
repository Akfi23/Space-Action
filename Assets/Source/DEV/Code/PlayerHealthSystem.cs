using Akfi;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kuhpik;
using Supyrb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : GameSystemWithScreen<GameScreen>
{
    private Color baseColor;

    public override void OnInit()
    {
        Signals.Get<HitOnPlayerSignal>().AddListener(HitPlayer);
        baseColor = screen.HealthIndicator.color;
        screen.PlayerHealthBar.InitialiseTargetIndicator(Camera.main, UIManager.Canvas, game.Player.gameObject);
    }

    public override void OnFixedUpdate()
    {
        game.Player.RegenHealth(config.PlayerConfig.HealthRegenBase + player.PlayerUpgradeDatas[UpgradeType.HealthRegen].UpgradeValue);
        screen.PlayerHealthText.text = game.Player.CurrentHealth.ToString("0.0");
        screen.PlayerHealthBar.UpdateTargetIndicator();
        float hpFill = 1f / game.Player.MaxHealth * game.Player.CurrentHealth;
        screen.PlayerHealthBar.CurrentHP.fillAmount = hpFill;
    }

    private async void HitPlayer(int damage)
    {
        screen.HealthIndicator.color = baseColor;
        screen.HealthIndicator.DOKill();
        screen.HealthIndicator.DOFade(0.35f, 0.5f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        await screen.UpdateHPBar(game.Player, screen.PlayerHealthBar, damage);
        screen.ShowDamageText(game.Player, screen.PlayerHealthBar, damage);

        if (game.Player.TakeDamage(damage) > 0) return;

        game.isAttack = false;
        game.Player.RigComponent.SetRig(false);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
    }
}
