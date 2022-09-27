using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class PlayerComponent : CharacterComponent
{
    private TriggerComponent trigger;
    private EnemyCheckerComponent enemyChecker;
    private RigIKComponent rigComponent;
    private ToolHolderComponent toolHolder;
    private Circle circle;

    public TriggerComponent Trigger => trigger;
    public EnemyCheckerComponent EnemyChecker => enemyChecker;
    public RigIKComponent RigComponent => rigComponent;
    public ToolHolderComponent ToolHolder => toolHolder;
    public Circle Circle => circle;

    public override void Init()
    {
        base.Init();

        circle = GetComponentInChildren<Circle>();

        trigger = GetComponent<TriggerComponent>();
        trigger.InitTrigger();

        enemyChecker = GetComponentInChildren<EnemyCheckerComponent>();
        enemyChecker.InitChecker();

        rigComponent = GetComponent<RigIKComponent>();

        toolHolder = GetComponent<ToolHolderComponent>();
        toolHolder.InitToolHolderComponent();
    }

    new public float TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        return currentHealth;
    }

    public void UpdateAttackRange(float radius)
    {
        enemyChecker.UpdateCollider(radius);
        circle.DrawCircle(enemyChecker.EnemyCheckerColl.radius);
    }

    public void UpdateHealthValue(float value)
    {
        maxHealth += value;
        currentHealth = maxHealth;
    }

    public void RegenHealth(float regenSpeed)
    {
        currentHealth = Mathf.Lerp(currentHealth, maxHealth, regenSpeed * Time.deltaTime);
    }
}
