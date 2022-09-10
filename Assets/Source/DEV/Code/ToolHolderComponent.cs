using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHolderComponent : MonoBehaviour
{
    [Header("Tool")]
    [HorizontalLine(2, EColor.White)]
    [SerializeField] private GameObject tool;
    [SerializeField] private Collider toolCollider;
    [Header("Gun")]
    [HorizontalLine(2, EColor.Indigo)]
    [SerializeField] private Transform gunHolder;
    [SerializeField] private Transform gunModel;
    [SerializeField] private Transform shootPoint;

    private ToolCollisionChecker toolCollisionChecker;

    public Transform GunHolder => gunHolder;
    public Transform GunModel => gunModel;
    public Transform ShootPoint => shootPoint;
    public GameObject Tool => tool;

    public void InitToolHolderComponent()
    {
        toolCollisionChecker = GetComponentInChildren<ToolCollisionChecker>();
        toolCollisionChecker.InitToolTrigger();
    }

    public void ToggleToolCollider(int status)
    {
        if (status == 0)
        {
            toolCollider.enabled = false;
        }
        else
        {
            toolCollider.enabled = true;
        }
    }
}
