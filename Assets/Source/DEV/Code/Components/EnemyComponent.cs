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

    [Button]
    public override void Init()
    {
        base.Init();
        dieSignal = Signals.Get<OnEnemyDie>();

        characterHUD = GetComponentInChildren<CharacterHUDComponent>();
        characterHUD.InitHUD();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out BulletComponent bullet)) return;

        TakeDamageByPlayer(bullet.transform.position,bullet);
    }

    private void TakeDamageByPlayer(Vector3 hitPos,BulletComponent bullet)
    {
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
        while (Time.time < t + 0.4f)
        {
            characterHUD.HUD.transform.localRotation = Quaternion.Euler(-transform.localRotation.eulerAngles);
            yield return null;
        }

        characterHUD.HUD.gameObject.SetActive(false);
    }
}
