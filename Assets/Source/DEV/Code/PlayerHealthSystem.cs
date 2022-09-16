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
    }

    private async void HitPlayer()
    {
        screen.HealthIndicator.color = baseColor;
        screen.HealthIndicator.DOKill();
        screen.HealthIndicator.DOFade(0.5f,0.5f).SetEase(Ease.Linear).SetLoops(2,LoopType.Yoyo);

        if (game.Player.TakeDamage() > 0) return;

        game.isAttack = false;
        game.Player.RigComponent.SetRig(false);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        Bootstrap.Instance.ChangeGameState(GameStateID.Lose);
    }
}
