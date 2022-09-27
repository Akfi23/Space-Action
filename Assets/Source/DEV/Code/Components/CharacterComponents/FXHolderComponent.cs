using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXHolderComponent : MonoBehaviour
{
    [SerializeField] private ParticleSystem jetpackFX;
    [SerializeField] private ParticleSystem landingFX;
    [SerializeField] private ParticleSystem startFlyFX;
    [SerializeField] private ParticleSystem runFX;
    [SerializeField] private ParticleSystem hitFX;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private ParticleSystem toolEffect;

    public ParticleSystem JetpackFX => jetpackFX;
    public ParticleSystem LandingFX => landingFX;
    public ParticleSystem StartFlyFX => startFlyFX;
    public ParticleSystem RunFX => runFX;
    public ParticleSystem HitFX => hitFX;
    public ParticleSystem ShootEffect => shootEffect;
    public ParticleSystem ToolEffect => toolEffect;

    public void SetHitPosition(Vector3 hitPosition)
    {
        hitFX.transform.position = hitPosition;
        hitFX.Play();
    }
}
