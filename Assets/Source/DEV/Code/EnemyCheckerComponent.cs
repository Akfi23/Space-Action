using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckerComponent : MonoBehaviour
{
    private OnEnemyInArea signal;

    public void InitChecker()
    {
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
}
