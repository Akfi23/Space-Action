using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Supyrb;
using System;
using UnityEngine;


public class EnemyComponent : CharacterComponent
{
    private int HitHash = Shader.PropertyToID("_PublicColor");
    private OnEnemyDie dieSignal;
    private CharacterHUDComponent characterHUD;
    private EnemyFSMComponent fsmComponent;
    private Vector3 bornPos;
    private HitOnPlayerSignal hitSignal;

    public EnemyFSMComponent FSM => fsmComponent;
    public Vector3 BornPos => bornPos;

    [Button]
    public override void Init()
    {
        base.Init();

        bornPos = transform.position;

        dieSignal = Signals.Get<OnEnemyDie>();
        hitSignal = Signals.Get<HitOnPlayerSignal>();

        characterHUD = GetComponentInChildren<CharacterHUDComponent>();
        characterHUD.InitHUD();

        fsmComponent = GetComponent<EnemyFSMComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out BulletComponent bullet)) return;

        TakeDamageByPlayer(bullet.transform.position,bullet);
    }

    private async void TakeDamageByPlayer(Vector3 hitPos,BulletComponent bullet)
    {
        //if (fsmComponent.GetState() == StateType.Attack)
        //    animator.SetEnemyAttack(false);

        TakeDamage();
        fx.SetHitPosition(hitPos);
        bullet.gameObject.SetActive(false);
        await ShowHpBar().ToCoroutine();
        await ShowHitEffect();
    }

    public override void Die()
    {
        base.Die();
        col.enabled = false;
        dieSignal.Dispatch(this);
        fx.LandingFX.Play();
        fsmComponent.SetState(StateType.Idle);
        agent.ResetPath();
    }

    public void PlayerAttackSignal()
    {
        hitSignal.Dispatch();
    }

    private async UniTask ShowHitEffect()
    {
        Color baseColor = Outline.FrontParameters.FillPass.GetColor(HitHash);
        Outline.FrontParameters.FillPass.SetColor(HitHash, Color.white);
        await UniTask.Delay(TimeSpan.FromSeconds(0.035f), ignoreTimeScale: true);
        Outline.FrontParameters.FillPass.SetColor(HitHash, baseColor);
    }

    private async UniTask ShowHpBar()
    {
        characterHUD.HUD.gameObject.SetActive(true);
        characterHUD.HPBar.fillAmount = 1f / maxHealth * currentHealth;

        float t = Time.time;

        while (Time.time < t + 0.45f)
        {
            characterHUD.HUD.transform.localRotation = Quaternion.Euler(-transform.localRotation.eulerAngles);
            await UniTask.Yield();
        }

        characterHUD.HUD.gameObject.SetActive(false);
    }
}
