using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyCheckerComponent : MonoBehaviour
{
    private OnEnemyInArea signal;
    private SphereCollider enemyCheckerColl;

    public SphereCollider EnemyCheckerColl => enemyCheckerColl;

    public void InitChecker()
    {
        enemyCheckerColl = GetComponent<SphereCollider>();
        signal = Signals.Get<OnEnemyInArea>();
    }

    private void OnTriggerEnter(Collider other)
    {
        signal.Dispatch(other.transform, true);
    }

    private void OnTriggerExit(Collider other)
    {
        signal.Dispatch(other.transform, false);
    }

    public void UpdateCollider(float radius)
    {
        enemyCheckerColl.radius = radius;
        Debug.Log("RADIUS " + radius);
    }
}
