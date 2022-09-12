using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSystem : GameSystemWithScreen<GameScreen>
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float flyPower;
    [SerializeField] private float flyTime;

    private bool isFly;
    private bool isFuelWarning;

    public override void OnInit()
    {
        Signals.Get<OnTriggerCollide>().AddListener(FlyByJetpack);
        screen.JetpackFuelFill.maxValue = 3;
        screen.JetpackFuelFill.value = screen.JetpackFuelFill.maxValue;
    }

    private void FlyByJetpack(Transform jumpTrigger, bool status)
    {
        if (!jumpTrigger.TryGetComponent(out JumpZoneComponent jumpComponent)) return;
        if (isFly) return;

        if (status == true)
        {
            if (screen.JetpackFuelFill.value >= 1)
            { 
                isFly = true;

                game.Player.Agent.enabled = false;

                game.Player.FX.JetpackFX.Play();
                game.Player.FX.RunFX.Stop();
                game.Player.FX.StartFlyFX.Play();
                game.Player.FX.StartFlyFX.transform.SetParent(null);
                game.Player.FX.StartFlyFX.transform.position = game.Player.transform.position;
                game.Player.Animator.SetFlyAnimStatus(true);

                game.Player.transform.DOLookAt(new Vector3(jumpComponent.EndFlyPos.position.x, game.Player.transform.position.y, jumpComponent.EndFlyPos.position.z), 0.15f).SetEase(Ease.InOutCubic);
                game.Player.transform.DOJump(jumpComponent.EndFlyPos.position, flyPower, 0, flyTime).OnComplete(() => StartCoroutine(OnJumpCompletedRoutine())).SetEase(_curve);

                if (game.Companion != null)
                {
                    game.Companion.Agent.ResetPath();
                    game.Companion.Agent.enabled = false;
                    game.Companion.transform.DOJump(jumpComponent.EndFlyPos.position, flyPower, 0, flyTime+1).OnComplete(() => StartCoroutine(OnJumpCompletedRoutine())).SetEase(_curve);
                }

                SpendJetpackFill();
            }
            else
            {
                ShowEmptyFuelWarning();
            }
        }
    }

    private IEnumerator OnJumpCompletedRoutine()
    {
        game.Player.FX.JetpackFX.Stop();
        game.Player.FX.LandingFX.Play();
        game.Player.Animator.SetFlyAnimStatus(false);
        game.Player.Agent.enabled = true;
        game.Companion.Agent.enabled = true;
        yield return new WaitForSeconds(0.2f);
        game.Player.FX.RunFX.Play();
        isFly = false;

        AccumulateJetpackFill();
    }

    private void SpendJetpackFill()
    {
        screen.JetpackFuelFill.DOKill();

        if (screen.JetpackFuelFill.value < screen.JetpackFuelFill.value + 1)
            screen.JetpackFuelFill.value = Mathf.RoundToInt(screen.JetpackFuelFill.value);

        screen.JetpackFuelFill.DOValue(screen.JetpackFuelFill.value - 1, flyTime).SetEase(Ease.Linear);
    }

    private void AccumulateJetpackFill()
    {
        float time = screen.JetpackFuelFill.maxValue - screen.JetpackFuelFill.value;
        screen.JetpackFuelFill.DOValue(screen.JetpackFuelFill.maxValue, time*10).SetEase(Ease.Linear);
    }

    private void ShowEmptyFuelWarning()
    {
        //screen.JetpackFuelIcon.color = Color.white;
        if (!isFuelWarning)
        {
            isFuelWarning = true;
            screen.JetpackFuelIcon.DOColor(screen.EmptyFuelColor, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(()=>isFuelWarning=false);
            screen.JetpackFuelIcon.transform.DOPunchScale(Vector3.one * 0.15f, 0.3f, 10, 1).SetEase(Ease.Linear)/*.SetLoops(2, LoopType.Yoyo)*/;
        }
    }
}
