using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class ToolCollisionChecker : MonoBehaviour
{
    private OnHitResource hitResourceSignal;

    [Button]
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
