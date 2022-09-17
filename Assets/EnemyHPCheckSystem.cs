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
            UpdateHPBar(enemy);
            ShowDamageText();

           yield return new WaitForSeconds(2f);

            screen.EnemyHealthBar.gameObject.SetActive(false);
        }

        private void UpdateHPBar(EnemyComponent enemy = null)
        {
            screen.EnemyHealthBar.UpdateTarget(enemy.gameObject);
            screen.EnemyHealthBar.gameObject.SetActive(true);
            screen.EnemyHealthBar.CurrentHP.fillAmount = 1f / enemy.MaxHealth * enemy.CurrentHealth;
            screen.EnemyHealthBar.FakeHP.fillAmount = 1f / enemy.MaxHealth * (enemy.CurrentHealth + 1);
            screen.EnemyHealthBar.FakeHP.DOFillAmount(screen.EnemyHealthBar.CurrentHP.fillAmount, 0.3f).SetEase(Ease.Linear);
        }

        private void ShowDamageText()
        {
            screen.DamageText.transform.DOKill();
            screen.DamageText.transform.localPosition = new Vector3(58, 0, 0);
            screen.DamageText.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => screen.DamageText.transform.DOScale(Vector3.one * 1.2f, 1.5f));
            screen.DamageText.DOFade(1, 0.5f).OnComplete(() => screen.DamageText.DOFade(0, 1.5f));
            screen.DamageText.transform.DOLocalMoveY(10f, 0.5f);
        }
    }
}
