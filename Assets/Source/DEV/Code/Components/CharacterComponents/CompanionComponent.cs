using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionComponent : MonoBehaviour
{
    [SerializeField] Transform companionModel;
    private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    public Transform CompanionModel => companionModel;

    public void Init()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Signals.Get<OnTriggerCollide>().Dispatch(other.transform,true);
    }

    private void OnTriggerExit(Collider other)
    {
        Signals.Get<OnTriggerCollide>().Dispatch(other.transform, false);
    }
}
