using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorComponent : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;

    private int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int FlyHash = Animator.StringToHash("IsFlying");
    private int DieHash = Animator.StringToHash("IsDie");

    public void InitAnimator()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoveSpeedAnimator(float multiplyer)
    {
        animator.SetFloat(MoveSpeedHash, multiplyer);
    }

    public void SetSideeOffsetAnimator(float offset)
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
}
