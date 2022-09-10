using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using NaughtyAttributes;

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
    //[Header("Gun")]
    //[HorizontalLine(2, EColor.Indigo)]
    //[SerializeField] private Transform gunHolder;
    //[SerializeField] private Transform gun;
    //[SerializeField] private Transform shootPoint;
    public RigBuilder RigBuilder => rigBuilder;
    public MultiAimConstraint HeadConstraint => headConstraint;
    public MultiAimConstraint SpineConstraint => spineConstraint;
    public Transform BodyTarget => bodyTarget;
    public Transform ArmTarget => armTarget;
    
    public void ActivateRig()
    {
        DOVirtual.Float(rigBody.weight, rigBody.weight + 1f, 1.5f, SetRigsWeight);
        DOVirtual.Float(rigBody.weight, rigBody.weight + 1f, 1.5f, SetRigsWeight);
    }

    public void DeactivateRig()
    {
        DOVirtual.Float(rigBody.weight, 0, 1.5f, SetRigsWeight)/*.OnComplete(() => gunHolder.transform.DOLocalRotate(new Vector3(-90f, -90, 180), 0.5f)*/;
    }

    private void SetRigsWeight(float value)
    {
        rigBody.weight = value;
        rigArm.weight = value;
    }
}
