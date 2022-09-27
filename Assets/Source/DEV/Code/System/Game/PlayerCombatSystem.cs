using Akfi;
using DG.Tweening;
using Kuhpik;
using Kuhpik.Pooling;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSystem : GameSystem
{
    [SerializeField] private EnemyComponent target;
    [SerializeField] private Transform targetMarker;

    private Transform gun;
    private Transform shootPoint;
    private float counter;

    public override void OnInit()
    {
        PrepareBullets();

        gun = game.Player.ToolHolder.GunHolder;
        shootPoint = game.Player.ToolHolder.ShootPoint;
        Signals.Get<OnEnemyInArea>().AddListener(ManageEnemyList);
        Signals.Get<OnEnemyHit>().AddListener(FindNextTargetAfterKill);
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
            GameObject projectile = PoolingSystem.GetObject(config.BulletPrefab.gameObject);
            game.Bullets.Add(projectile);
        }

        foreach (var bullet in game.Bullets)
        {
            bullet.SetActive(false);
        }
    }

    private void TryToShoot()
    {
        counter += Time.deltaTime;

        if (counter >= config.FireRate - player.PlayerUpgradeDatas[UpgradeType.AttackSpeed].UpgradeValue)
        {
            GetBullet();
            game.Player.FX.ShootEffect.Play();
            counter = 0;
        }
    }

    private GameObject GetBullet()
    {
        GameObject projectile = PoolingSystem.GetObject(config.BulletPrefab.gameObject);
        projectile.transform.position = shootPoint.transform.position;
        projectile.transform.forward = gun.forward;

        if (projectile.TryGetComponent(out BulletComponent bullet))
        {
            bullet.Damage = config.PlayerConfig.DamageBase + player.PlayerUpgradeDatas[UpgradeType.Damage].UpgradeValue;
        }

        return projectile;
    }

    private void Aim()
    {
        game.Player.RigComponent.ArmTarget.position = target.transform.position + Vector3.up * 1.3f;
        game.Player.RigComponent.BodyTarget.position = target.transform.position + Vector3.up * 2f;

        Quaternion rot = Quaternion.LookRotation(game.Player.RigComponent.ArmTarget.position - gun.transform.position);
        gun.rotation = rot;

        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - game.Player.transform.position);
        game.Player.transform.rotation = Quaternion.Lerp(game.Player.transform.rotation, lookRotation, 5 * Time.deltaTime);
    }

    private void ManageEnemyList(Transform enemy, bool status)
    {
        if (!enemy.TryGetComponent(out EnemyComponent enemyComponent)) return;

        enemyComponent.FSM.SetState(StateType.Chase);

        if (status)
        {
            AddEnemyToList(enemyComponent);
        }
        else
        {
            RemoveEnemyFromList(enemyComponent);
            enemyComponent.FSM.SetState(StateType.GoBack);
        }

        TryAttackEnemy();
    }

    private void AddEnemyToList(EnemyComponent enemyComponent)
    {
        game.EnemiesInArea.Add(enemyComponent);
    }

    private void RemoveEnemyFromList(EnemyComponent enemyComponent)
    {
        game.EnemiesInArea.Remove(enemyComponent);
    }

    private void TryAttackEnemy()
    {
        if (game.EnemiesInArea.Count > 0)
        {
            if (game.EnemiesInArea[0].CurrentHealth > 0)
            {
                target = game.EnemiesInArea[0];
                AttachMarkerToTarget();
                StartShooting();
            }
        }
        else
        {
            DetachMarker();
            StopShooting();
        }
    }

    private void FindNextTargetAfterKill(EnemyComponent enemy)
    {
        if (enemy.CurrentHealth > 0) return;
        RemoveEnemyFromList(enemy);
        TryAttackEnemy();
    }

    private void StartShooting()
    {
        game.isAttack = true;
        game.Player.RigComponent.SetRig(true);
        game.Player.Animator.SetHitLayer(false);
        game.Player.ToolHolder.GunHolder.gameObject.SetActive(true);
        game.Player.ToolHolder.Tool.gameObject.SetActive(false);
        cameraController.SetCombatCameraActive();
    }

    private void StopShooting()
    {
        game.isAttack = false;
        game.Player.RigComponent.SetRig(false);
        gun.transform.DOLocalRotate(new Vector3(-90f, -90, 180), 0.5f)
            .OnComplete(() => game.Player.ToolHolder.GunHolder.gameObject.SetActive(false));
        cameraController.SetGameCameraActive();
    }

    private void MoveBullets()
    {
        if (game.Bullets.Count > 0)
        {
            foreach (var bul in game.Bullets)
            {
                if (bul.activeSelf)
                    bul.transform.position += bul.transform.forward * Time.deltaTime * 20;
            }
        }
    }

    private void AttachMarkerToTarget()
    {
        targetMarker.gameObject.SetActive(true);
        targetMarker.transform.SetParent(target.transform);
        targetMarker.transform.localPosition = Vector3.zero;
    }

    private void DetachMarker()
    {
        targetMarker.SetParent(null);
        targetMarker.gameObject.SetActive(false);
    }

}
