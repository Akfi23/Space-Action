using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AnimatorComponent : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;

    private int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int FlyHash = Animator.StringToHash("IsFlying");
    private int DieHash = Animator.StringToHash("IsDie");

    private CompositeDisposable disposable = new CompositeDisposable();

    public void InitAnimator()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoveSpeedAnimator(float multiplyer)
    {
        animator.SetFloat(MoveSpeedHash, multiplyer);
    }

    public void SetSideOffsetAnimator(float offset)
    {
        //animator.SetFloat(SideOffsetHash, offset);
    }

    public void SetFlyAnimStatus(bool status)
    {
        animator.SetBool(FlyHash, status);
    }

    public void SetDie()
    {
        animator.SetTrigger(DieHash);
    }

    public void OnHitLayer()
    {
        float currentWeight = animator.GetLayerWeight(1);
        disposable.Clear();

        Observable.EveryUpdate().Subscribe(_ =>
        {
            currentWeight += Time.deltaTime * 5;
            animator.SetLayerWeight(1, currentWeight);

            if (currentWeight >= 1)
            {
                disposable.Clear();
            }
        }).AddTo(disposable);
    }

    public void OffHitLayer()
    {
        float currentWeight = animator.GetLayerWeight(1);
        disposable.Clear();

        Observable.EveryUpdate().Subscribe(_ =>
        {
            currentWeight -= Time.deltaTime * 5;
            animator.SetLayerWeight(1, currentWeight);

            if (currentWeight <= 0)
            {
                disposable.Clear();
            }
        }).AddTo(disposable);
    }
}
