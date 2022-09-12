using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResourceObjectComponent : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private GameObject[] objectParts;
    [SerializeField] private Collider col;
    [SerializeField] private NavMeshObstacle obstacle;
    [SerializeField] private int hitCounter;
    [SerializeField] private GameObject effectObject;

    public ResourceType Type => type;
    public GameObject EffectObject => effectObject;
    public int HitCounter=>hitCounter;

    public void OnInit()
    {
        col = GetComponent<Collider>();
        obstacle = GetComponent<NavMeshObstacle>();
        effectObject.transform.SetParent(null);
    }

    public void IncreaseHitCounter() 
    {
        hitCounter++;
    }

    public void RenewHitCount()
    {
        hitCounter = 0;
    }

    public int GetModelsCount()
    {
        return objectParts.Length;
    }

    public void HideModelPart()
    {
        objectParts[hitCounter].SetActive(false);
    }

    public void SwitchObjectActiveStatus(bool status)
    {
        col.enabled = status;
        obstacle.enabled = status;

        if (!status) return;

        foreach (var item in objectParts)
        {
            item.gameObject.SetActive(true);
        }
    }

    public Vector3 GetCurrentPartPosition()
    {
        return objectParts[hitCounter-1].transform.position;
    }
}
