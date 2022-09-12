using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigIKComponent : MonoBehaviour
{
    [Header("RIG Stuff")]
    [HorizontalLine(2,EColor.Green)]
    [SerializeField] private RigBuilder rigBuilder;
    [SerializeField] private MultiAimConstraint headConstraint;
    [SerializeField] private MultiAimConstraint spineConstraint;
    [SerializeField] private Rig rigBody;
    [SerializeField] private Rig rigArm;
    [Header("Bones")]
    [HorizontalLine(2, EColor.Blue)]
    [SerializeField] private Transform bodyTarget;
    [SerializeField] private Transform armTarget;

    public RigBuilder RigBuilder => rigBuilder;
    public MultiAimConstraint HeadConstraint => headConstraint;
    public MultiAimConstraint SpineConstraint => spineConstraint;
    public Transform BodyTarget => bodyTarget;
    public Transform ArmTarget => armTarget;

    private Tweener tweener;

    public void SetRig(bool status)
    {
        if (tweener != null)
            tweener.Kill();

        float tweenTime = 0;
        float targetWeight = 0;

        if (status)
        {
            tweenTime = 1;
            targetWeight = 1;
        }
        else
        {
            tweenTime = 0.5f;
        }

        if (targetWeight == rigBody.weight) return;

        tweener = DOVirtual.Float(rigBody.weight, targetWeight, tweenTime, SetRigsWeight);
    }

    private void SetRigsWeight(float value)
    {
        rigBody.weight = value;
        rigArm.weight = value;
    }
}
