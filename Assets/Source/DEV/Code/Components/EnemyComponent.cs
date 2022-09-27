using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Supyrb;
using System;
using UnityEngine;


public class EnemyComponent : CharacterComponent
{
    [SerializeField] private int damage;
    [SerializeField] private int reward;
    private int HitHash = Shader.PropertyToID("_PublicColor");
    private OnEnemyHit hitSignal;
    private EnemyFSMComponent fsmComponent;
    private Vector3 bornPos;
    private HitOnPlayerSignal attackSignal;

    public EnemyFSMComponent FSM => fsmComponent;
    public Vector3 BornPos => bornPos;
    public int Damage => damage;
    public int Reward => reward;

    [Button]
    public override void Init()
    {
        base.Init();

        bornPos = transform.position;

        hitSignal = Signals.Get<OnEnemyHit>();
        attackSignal = Signals.Get<HitOnPlayerSignal>();
        fsmComponent = GetComponent<EnemyFSMComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out BulletComponent bullet)) return;

        TakeDamageByPlayer(bullet.transform.position,bullet);
    }

    private async void TakeDamageByPlayer(Vector3 hitPos, BulletComponent bullet)
    {
        //if (fsmComponent.GetState() == StateType.Attack)
        //    animator.SetEnemyAttack(false);

        TakeDamage(bullet.Damage);
        fx.SetHitPosition(hitPos);
        bullet.gameObject.SetActive(false);
        await ShowHitEffect();
        hitSignal.Dispatch(this);
    }

    public override void Die()
    {
        base.Die();
        col.enabled = false;
        fx.LandingFX.Play();
        fsmComponent.SetState(StateType.Idle);
        agent.ResetPath();
    }

    public void PlayerAttackSignal()
    {
        attackSignal.Dispatch(damage);
    }

    private async UniTask ShowHitEffect()
    {
        Color baseColor = Outline.FrontParameters.FillPass.GetColor(HitHash);
        Outline.FrontParameters.FillPass.SetColor(HitHash, Color.white);
        await UniTask.Delay(TimeSpan.FromSeconds(0.035f), ignoreTimeScale: true);
        Outline.FrontParameters.FillPass.SetColor(HitHash, baseColor);
    }   
}
