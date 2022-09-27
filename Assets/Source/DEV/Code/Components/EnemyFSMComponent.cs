using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMComponent : MonoBehaviour
{
    private NoTransitionFSM<StateType, CharacterState> fsm;

    [SerializeField] private List<CharacterState> states;
    [SerializeField] private EnemyComponent enemy;
    [SerializeField] private StateType initialState;
    [SerializeField] CharacterState currentState;

    public void Init(GameData data)
    {
        enemy = gameObject.GetComponent<EnemyComponent>();
        fsm = new NoTransitionFSM<StateType, CharacterState>();

        foreach (var state in states)
        {
            fsm.AddState(state.Type, state);
            state.Init(fsm, data);
        }

        SetState(initialState);
    }

    public StateType GetState()
    {
        return fsm.GetCurrentState();
    }

    public void SetState(StateType stateType)
    {
        fsm.ChangeState(stateType);
        fsm.State.OnStateEnter(enemy);
        currentState = fsm.State;
    }

    public void Work()
    {
        fsm.State.Work(enemy);
    }

    public bool FindAvailableState(StateType type)
    {
        return fsm.FindState(type);
    }
}
