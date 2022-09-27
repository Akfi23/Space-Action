using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using Kuhpik.Pooling;
using Supyrb;
using UnityEngine;

namespace Akfi
{
    public class MoneyCollectSystem : GameSystemWithScreen<GameScreen>
    {
        [SerializeField] private GameObject coinPrefab;
        public override void OnInit()
        {
            Signals.Get<OnEnemyHit>().AddListener(TryGetMoney);
        }

        private void TryGetMoney(EnemyComponent enemy)
        {
            if (enemy.CurrentHealth > 0) return;

            StartCoroutine(GetMoney(enemy));
        }

        private IEnumerator GetMoney(EnemyComponent enemy)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject coin = PoolingSystem.GetObject(coinPrefab);
                coin.transform.position = enemy.transform.position;

                Vector3 pos = new Vector3(Random.Range(enemy.transform.position.x - 5, enemy.transform.position.x + 5), enemy.transform.position.y - 1,
                    Random.Range(enemy.transform.position.z - 5, enemy.transform.position.z + 5));

                Sequence sequence = DOTween.Sequence();
                sequence.Append(coin.transform.DOJump(pos, 1, 1, 0.25f));
                sequence.Append(coin.transform.DOJump(game.Player.transform.position, 1, 1, 0.15f).SetDelay(0.15f));
                sequence.AppendCallback(() => coin.gameObject.SetActive(false));
            }

            yield return new WaitForSeconds(0.65f);

            player.Money += enemy.Reward;
            screen.UpdateMoney(player.Money);
        }
    }
}
