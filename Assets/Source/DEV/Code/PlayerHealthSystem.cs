using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : GameSystemWithScreen<GameScreen>
{
    public override void OnInit()
    {
        Signals.Get<HitOnPlayerSignal>().AddListener(HitPlayer);
    }

    private void HitPlayer()
    {
        screen.HealthIndicator.DOKill();
        screen.HealthIndicator.DOFade(0.5f,0.5f).SetEase(Ease.Linear).SetLoops(2,LoopType.Yoyo);
        game.Player.TakeDamage();
    }
}
