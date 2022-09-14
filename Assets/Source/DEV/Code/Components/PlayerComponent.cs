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
        circle.DrawCircle(7.5f);

        trigger = GetComponent<TriggerComponent>();
        trigger.InitTrigger();

        enemyChecker = GetComponentInChildren<EnemyCheckerComponent>();
        enemyChecker.InitChecker();

        rigComponent = GetComponent<RigIKComponent>();

        toolHolder = GetComponent<ToolHolderComponent>();
        toolHolder.InitToolHolderComponent();
    }
}
