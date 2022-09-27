using DG.Tweening;
using System.Collections;
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
    [SerializeField] private Transform root;

    public ResourceType Type => type;
    public GameObject EffectObject => effectObject;
    public int HitCounter=>hitCounter;
    public Transform Root => root;

    public void OnInit()
    {
        //col = GetComponent<Collider>();
        //obstacle = GetComponent<NavMeshObstacle>();
        //effectObject.transform.SetParent(null);
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
        Debug.Log(objectParts[hitCounter - 1].transform.position);
        return objectParts[hitCounter-1].transform.position;
    }
}
