using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class EnemyStates : State<BaseEnemy>
{
    protected EnemyState state;
    protected BaseEnemy enemy;
    public override void Init(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }
    public EnemyState GetState()
    {
        return state;
    }
}

