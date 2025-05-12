using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    protected float elapsedTime;

    public State() { }
    public virtual void Init(T owner) { }
    public virtual void OnEnter() 
    {
        elapsedTime = 0;
        Debug.Log(this.GetType().Name + " Enter");
    }
    public virtual void OnExit() { }
    public virtual void OnUpdate(float deltaTime) 
    {
        elapsedTime += deltaTime;
    }
    public virtual void OnFixedUpdate() { }
}
