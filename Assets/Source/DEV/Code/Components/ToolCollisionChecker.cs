using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCollisionChecker : MonoBehaviour
{
    private OnHitResource hitResourceSignal;

    public void InitToolTrigger()
    {
        hitResourceSignal = Signals.Get<OnHitResource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out ResourceObjectComponent resource)) return;

        hitResourceSignal.Dispatch(resource);
    }
}
