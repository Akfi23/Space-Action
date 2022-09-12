using DG.Tweening;
using Kuhpik;
using Kuhpik.Pooling;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSystem : GameSystem
{
    [SerializeField] private EnemyComponent target;
    [SerializeField] private GameObject bullet;

    private List<EnemyComponent> enemies = new List<EnemyComponent>();
    private Transform gun;
    private Transform shootPoint;
    private float counter;

    public override void OnInit()
    {
        PrepareBullets();

        gun = game.Player.ToolHolder.GunHolder;
        shootPoint = game.Player.ToolHolder.ShootPoint;
        Signals.Get<OnEnemyInArea>().AddListener(ManageEnemyList);
        Signals.Get<OnEnemyDie>().AddListener(FindNextTargetAfterKill);
    }

    public override void OnUpdate()
    {
        MoveBullets();

        if (!game.isAttack) return;

        TryToShoot();
        Aim();
    }

    private void PrepareBullets()
    {
        int poolCapacity = PoolInstaller.Instance.GetPool("Bullet").Capacity;

        for (int i = 0; i < poolCapacity; i++)
        {
            GameObject projectile = PoolingSystem.GetObject(bullet);
            game.Bullets.Add(projectile);
        }

        foreach (var bullet in game.Bullets)
        {
            bullet.SetActive(false);
        }
    }

    public override void OnGameEnd()
    {
        PoolingSystem.Clear();
    }

    private void TryToShoot()
    {
        counter += Time.deltaTime;

        if (counter >= 0.7)
        {
            GameObject projectile = GetBullet();
            game.Player.FX.ShootEffect.Play();
            counter = 0;
        }
    }

    private GameObject GetBullet()
    {
        GameObject projectile = PoolingSystem.GetObject(bullet);
        projectile.transform.position = shootPoint.transform.position;
        projectile.transform.forward = gun.forward;

        return projectile;
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
                StartShooting();
            }
        }
        else
        {
            StopShooting();
        }
    }

    private void FindNextTargetAfterKill(EnemyComponent enemy)
    {
        RemoveEnemyFromList(enemy);
        TryAttackEnemy();
    }

    private void StartShooting()
    {
        game.isAttack = true;
        game.Player.RigComponent.ActivateRig();
        game.Player.Animator.OffHitLayer();
        game.Player.ToolHolder.GunHolder.gameObject.SetActive(true);
        game.Player.ToolHolder.Tool.gameObject.SetActive(false);
    }

    private void StopShooting()
    {
        game.isAttack = false;
        game.Player.RigComponent.DeactivateRig();
        gun.transform.DOLocalRotate(new Vector3(-90f, -90, 180), 0.5f)
            .OnComplete(() => game.Player.ToolHolder.GunHolder.gameObject.SetActive(false));
    }

    private void MoveBullets()
    {
        if (game.Bullets.Count > 0)
        {
            foreach (var bul in game.Bullets)
            {
                if(bul.activeSelf)
                    bul.transform.position += bul.transform.forward * Time.deltaTime * 20;
            }
        }
    }
}