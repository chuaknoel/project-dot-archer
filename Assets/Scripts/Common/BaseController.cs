using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController<T> where T: MonoBehaviour
{
    public Dictionary<string, State<T>> registedSatae = new Dictionary<string, State<T>>();

    protected State<T> currentState;

    protected State<T> previousState;

    public BaseController(State<T> initState, T ower)
    {
        RegisterState(initState , ower);

        currentState = initState;
        currentState.OnEnter();
    }

    public virtual void RegisterState(State<T> state, T owner)
    {
        state.Init(owner);
        registedSatae[state.GetType().Name] = state;
    }

    public virtual void OnUpdate(float deltaTime)
    {
        currentState?.OnUpdate(deltaTime);
    }

    public virtual void OnFixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }

    public virtual void ChangeState(string newState)
    {
        if(currentState.ToString() == newState) return;

        currentState?.OnExit();
        
        previousState = currentState;

        currentState = registedSatae[newState];
        currentState.OnEnter();
    }

    public State<T> GetState()
    {
        return currentState;
    }
}