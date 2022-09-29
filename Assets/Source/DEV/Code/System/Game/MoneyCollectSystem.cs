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
                sequence.AppendCallback(() => StartCoroutine(MoveCoinToPlayer(coin)));
            }

            yield return new WaitForSeconds(0.65f);

            player.Money += enemy.Reward;
            screen.UpdateMoney(player.Money);
        }

        private IEnumerator MoveCoinToPlayer(GameObject coin)
        {
            while (coin.activeSelf)
            {
                yield return null;
                coin.transform.position = Vector3.Slerp(coin.transform.position, game.Player.transform.position+Vector3.up*2f, 50 * Time.deltaTime);
            }

            coin.SetActive(false);
        }
    }
}
