using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    public class EnemyBossIndicatorSystem : GameSystemWithScreen<GameScreen>
    {
        [SerializeField] private GameObject enemyBoss;
        public override void OnInit()
        {
            screen.BossIndicator.InitialiseTargetIndicator(Camera.main, UIManager.Canvas, enemyBoss);
            screen.BossIndicator.gameObject.SetActive(true);
        }

        public override void OnUpdate()
        {
            screen.BossIndicator.UpdateTargetIndicator();
        }
    }
}
