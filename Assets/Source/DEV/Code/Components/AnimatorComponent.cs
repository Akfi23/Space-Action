using DG.Tweening;
using UniRx;
using UnityEngine;

public class AnimatorComponent : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;

    private int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int FlyHash = Animator.StringToHash("IsFlying");
    private int DieHash = Animator.StringToHash("IsDie");

    private Tweener tweener;
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

    public void SetHitLayer(bool status)
    {
        if (tweener != null)
            tweener.Kill();

        float tweenTime = 0;
        float targetWeight = 0;
        float currentWeight = animator.GetLayerWeight(1);

        if (status)
        {
            tweenTime = 1;
            targetWeight = 1;
        }
        else
        {
            tweenTime = 0.5f;
        }

        if (targetWeight == currentWeight) return;

        tweener = DOVirtual.Float(currentWeight, targetWeight, tweenTime, SetLayerHitLayerWeight);
    }

    private void SetLayerHitLayerWeight(float weight)
    {
        animator.SetLayerWeight(1, weight);
    }
}
