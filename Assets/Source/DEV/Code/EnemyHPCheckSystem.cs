using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akfi
{
    public class EnemyHPCheckSystem : GameSystemWithScreen<GameScreen>
    {
        private IEnumerator hpBarRoutine;
        private Camera mainCamera;
        private Canvas canvas;

        public override void OnInit()
        {
            Signals.Get<OnEnemyHit>().AddListener(ShowEnemyHP);
            mainCamera = Camera.main;
            canvas = UIManager.Canvas;
            screen.EnemyHealthBar.InitialiseTargetIndicator(mainCamera, canvas);
        }

        public override void OnUpdate()
        {
            if (!screen.EnemyHealthBar.gameObject.activeSelf) return;

            screen.EnemyHealthBar.UpdateTargetIndicator();
        }

        private void ShowEnemyHP(EnemyComponent enemy)
        {
            if (hpBarRoutine != null)
            {
                StopCoroutine(hpBarRoutine);
            }

            hpBarRoutine = HPBarRoutine(enemy);
            StartCoroutine(hpBarRoutine);
        }

        private IEnumerator HPBarRoutine(EnemyComponent enemy = null)
        {
            screen.EnemyHealthBar.UpdateTarget(enemy.gameObject);
            screen.EnemyHealthBar.gameObject.SetActive(true);
            screen.UpdateHPBar(enemy, screen.EnemyHealthBar, config.PlayerConfig.DamageBase + player.PlayerUpgradeDatas[UpgradeType.Damage].UpgradeValue);
            screen.ShowDamageText(enemy, screen.EnemyHealthBar, config.PlayerConfig.DamageBase + player.PlayerUpgradeDatas[UpgradeType.Damage].UpgradeValue);

            yield return new WaitForSeconds(2f);

            screen.EnemyHealthBar.gameObject.SetActive(false);
        }
    }
}

