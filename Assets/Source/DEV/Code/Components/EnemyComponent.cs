using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
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

    private void TakeDamageByPlayer(Vector3 hitPos,BulletComponent bullet)
    {
        if (fsmComponent.GetState() == StateType.Attack)
            animator.SetEnemyAttack(false);

        TakeDamage();
        fx.SetHitPosition(hitPos);
        StartCoroutine(HPBarRoutine());
        StartCoroutine(HitEffectRoutine());
        bullet.gameObject.SetActive(false);
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

    private IEnumerator HitEffectRoutine()
    {
        Color baseColor = Outline.FrontParameters.FillPass.GetColor(HitHash);
        Outline.FrontParameters.FillPass.SetColor(HitHash, Color.white);
        yield return new WaitForSeconds(0.1f);
        Outline.FrontParameters.FillPass.SetColor(HitHash, baseColor);
    }

    IEnumerator HPBarRoutine()
    {
        characterHUD.HUD.gameObject.SetActive(true);
        characterHUD.HPBar.fillAmount = 1f / maxHealth * currentHealth;

        float t = Time.time;
        while (Time.time < t + 0.45f)
        {
            characterHUD.HUD.transform.localRotation = Quaternion.Euler(-transform.localRotation.eulerAngles);
            yield return null;
        }

        characterHUD.HUD.gameObject.SetActive(false);
    }
}
