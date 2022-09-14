using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoTransitionFSM<TKey, TState>
{
    public TState State { get; private set; }

    TKey currentStateKey;
    Dictionary<TKey, TState> states = new Dictionary<TKey, TState>();

    public void AddState(TKey key, TState state)
    {
        states.Add(key, state);
    }

    public TState GetState(TKey key)
    {
        return states[key];
    }

    public void ChangeState(TKey key)
    {
        //Debug.Log($"State changed to {key}!");
        State = states[key];
        currentStateKey = key;
    }

    public async void ChangeState(TKey key, float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
        ChangeState(key);
    }

    public void SetState(TKey key)
    {
        if (State != null) return;

        currentStateKey = key;
        State = states[key];
    }

    public TKey GetCurrentState()
    {
        return currentStateKey;
    }

    public bool FindState(TKey key)
    {
        if (states.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
