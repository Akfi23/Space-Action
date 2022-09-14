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
            GameObject projectile = PoolingSystem.GetObject(config.BulletPrefab.gameObject);
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

        if (counter >= config.FireRate)
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
        game.EnemiesInArea.Add(enemyComponent);
    }

    private void RemoveEnemyFromList(EnemyComponent enemyComponent)
    {
        game.EnemiesInArea.Remove(enemyComponent);
    }

    private void TryAttackEnemy()
    {
        counter = 0;

        if (game.EnemiesInArea.Count > 0)
        {
            if (game.EnemiesInArea[0].CurrentHealth > 0)
            {
                target = game.EnemiesInArea[0];
                target.FSM.SetState(StateType.Chase);
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
                if(bul.activeSelf)
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
