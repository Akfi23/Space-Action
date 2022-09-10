using NaughtyAttributes;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : CharacterComponent
{
    private int HitHash = Shader.PropertyToID("_PublicColor");
    private OnEnemyDie dieSignal;

    [Button]
    public override void Init()
    {
        base.Init();
        dieSignal = Signals.Get<OnEnemyDie>();
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
}
