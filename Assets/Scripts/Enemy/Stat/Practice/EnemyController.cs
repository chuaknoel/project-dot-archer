using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class EnemyController : BaseController<BaseEnemy>
{
    protected BaseEnemy enemy;
    protected EnemyStat enemyStat;
    
    public EnemyController(State<BaseEnemy> initState, BaseEnemy enemy) : base(initState, enemy)
    {
        this.enemy = enemy;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    

}

