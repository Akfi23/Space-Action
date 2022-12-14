using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResourceSystem : GameSystemWithScreen<GameScreen>
{
    [SerializeField] private List<ResourceObjectComponent> resources = new List<ResourceObjectComponent>();
    [SerializeField] private float interactingMagnitude;

    public override void OnInit()
    {
        Signals.Get<OnHitResource>().AddListener(StartCollectRoutine);
        Signals.Get<OnTriggerCollide>().AddListener(OnResourceCollide);
    }

    private void OnResourceCollide(Transform target, bool status)
    {
        if (!target.TryGetComponent(out ResourceObjectComponent resource)) return;
        if (game.isAttack) return;

        if (status && !resources.Contains(resource))
            resources.Add(resource);
        else if (!status && resources.Contains(resource))
            resources.Remove(resource);

        FindAvailableResources();
    }

    private void StartCollectRoutine(ResourceObjectComponent resource)
    {
        StartCoroutine(CollectResourceByHit(resource));
        StartCoroutine(ShakingRoutine(resource));
    }

    private IEnumerator CollectResourceByHit(ResourceObjectComponent resource)
    {
        if (resource.HitCounter < resource.GetModelsCount())
        {
            resource.HideModelPart();
            resource.IncreaseHitCounter();
            game.Player.FX.ToolEffect.transform.SetParent(resource.transform);
            game.Player.FX.ToolEffect.transform.position = resource.GetCurrentPartPosition();
            game.Player.FX.ToolEffect.Play();
            ThrowResourceToPlayer(resource);
        }

        yield return new WaitForSeconds(0.1f);

        if (resource.HitCounter == resource.GetModelsCount())
        {
            StartCoroutine(RespawnRoutine(resource));
        }
    }

    private IEnumerator ShakingRoutine(ResourceObjectComponent resource)
    {
        Vector3 RandomVec;

        for (float i = 2.0f; i >= 0; i -= 0.5f)
        {
            RandomVec = FastExtensions.RandomVector3(-1, 1, 0, 0, -1, 1).normalized * interactingMagnitude * i;
            RandomVec.y = resource.Root.eulerAngles.y;
            resource.Root.DORotate(RandomVec, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RespawnRoutine(ResourceObjectComponent resource)
    {
        if (resources.Contains(resource))
        {
            Debug.Log(resource.Type);
            resources.Remove(resource);
        }

        FindAvailableResources();

        resource.SwitchObjectActiveStatus(false);
        yield return new WaitForSeconds(config.ResourceRespawnTime);
        resource.RenewHitCount();
        resource.SwitchObjectActiveStatus(true);
        resource.transform.localScale = Vector3.one * 0.15f;
        resource.transform.DOScale(Vector3.one /** 0.333f*/, 0.25f);
    }

    private void ThrowResourceToPlayer(ResourceObjectComponent resource)
    {
        resource.EffectObject.transform.DOKill(resource.EffectObject);
        resource.EffectObject.transform.position = resource.GetCurrentPartPosition();
        resource.EffectObject.transform.localScale = new Vector3(1, 1, 1);
        resource.EffectObject.SetActive(true);
        resource.EffectObject.transform.DORotate(FastExtensions.RandomVector3(), 0.5f, RotateMode.FastBeyond360);
        resource.EffectObject.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
        resource.EffectObject.transform.DOLocalMoveY(5f, 0.2f).OnComplete(() => resource.EffectObject.transform.DOMove(game.Player.transform.position, 0.35f).OnComplete(() => resource.EffectObject.SetActive(false)));
    }

    private void FindAvailableResources()
    {
        game.Player.ToolHolder.ToggleToolCollider(0);

        if (game.isAttack) return;

        if (resources.Count > 0)
        {
            game.Player.Animator.SetHitLayer(true);

            game.Player.ToolHolder.Tool.gameObject.SetActive(true);
            game.Player.ToolHolder.GunHolder.gameObject.SetActive(false);
        }
        else
        {
            game.Player.Animator.SetHitLayer(false);
            game.Player.ToolHolder.Tool.gameObject.SetActive(false);
        }
    }
}
