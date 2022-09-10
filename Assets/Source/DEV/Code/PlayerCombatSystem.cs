using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerCombatSystem : GameSystem
{
    private CompositeDisposable disposable = new CompositeDisposable();

    [SerializeField] private EnemyComponent target;
    [SerializeField] private GameObject bullet;

    private List<EnemyComponent> enemies = new List<EnemyComponent>();
    private List<GameObject> bullets = new List<GameObject>();
    private Transform gun;
    private Transform shootPoint;
    private float counter;

    public override void OnInit()
    {
        gun = game.Player.ToolHolder.GunHolder;
        shootPoint = game.Player.ToolHolder.ShootPoint;
        Signals.Get<OnEnemyInArea>().AddListener(ManageEnemyList);
        Signals.Get<OnEnemyDie>().AddListener(FindNextTargetAfterKill);
    }

    private void Shoot()
    {
        counter += Time.deltaTime;

        if (counter >= 1)
        {
            var projectile = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity, null); // there will be Pool
            projectile.transform.forward = gun.forward;
            game.Player.FX.ShootEffect.Play();

            bullets.Add(projectile);
            counter = 0;
        }

        if (bullets.Count > 0)
        {
            foreach (var bul in bullets)
            {
                bul.transform.position += bul.transform.forward * Time.deltaTime * 10;
            }
        }
    }

    private void Aim()
    {
        game.Player.RigComponent.ArmTarget.position = target.transform.position + Vector3.up * 1.3f;
        game.Player.RigComponent.BodyTarget.position = target.transform.position + Vector3.up * 2f;
        Quaternion rot = Quaternion.LookRotation(game.Player.RigComponent.ArmTarget.position - gun.transform.position);
        gun.rotation = rot;
    }

    private void ManageEnemyList(Transform enemy, bool status)
    {
        if (!enemy.TryGetComponent(out EnemyComponent enemyComponent)) return;

        if (status)
        {
            AddEnemyToList(enemyComponent);
        }
        else
        {
            RemoveEnemyFromList(enemyComponent);
        }

        TryAttackEnemy();
    }

    private void AddEnemyToList(EnemyComponent enemyComponent)
    {
        enemies.Add(enemyComponent);
    }

    private void RemoveEnemyFromList(EnemyComponent enemyComponent)
    {
        enemies.Remove(enemyComponent);
    }

    private void TryAttackEnemy()
    {
        if (enemies.Count > 0)
        {
            if (enemies[0].CurrentHealth > 0)
            {
                target = enemies[0];
                game.isAttack = true;
                StartShooting();
                game.Player.RigComponent.ActivateRig();
                game.Player.Animator.OffHitLayer();
                game.Player.ToolHolder.GunHolder.gameObject.SetActive(true);
                game.Player.ToolHolder.Tool.gameObject.SetActive(false);
            }
        }
        else
        {
            game.isAttack = false;
            StopShooting();
            game.Player.RigComponent.DeactivateRig();

            gun.transform.DOLocalRotate(new Vector3(-90f, -90, 180), 0.5f)
                .OnComplete(() => game.Player.ToolHolder.GunHolder.gameObject.SetActive(false));
        }
    }

    private void FindNextTargetAfterKill(EnemyComponent enemy)
    {
        RemoveEnemyFromList(enemy);
        TryAttackEnemy();
    }

    private void StartShooting()
    {
        Observable.EveryUpdate().Subscribe(_ => { Aim(); }).AddTo(disposable);
        Observable.EveryUpdate().Subscribe(_ => { Shoot(); }).AddTo(disposable);
    }

    private void StopShooting()
    {
        disposable.Clear();
    }
}
