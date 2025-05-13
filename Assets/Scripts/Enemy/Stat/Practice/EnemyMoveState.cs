using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class EnemyMoveState : EnemyStates
{

    public override void Init(BaseEnemy enemy)
    {
        base.Init(enemy);
        state = EnemyState.Move;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //enemy.ChangeAnime(EnemyState.Move);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        enemy.Controller.MoveToPlayer(enemy.target, 1f);
    }


    public override void OnFixedUpdate()
    {
        
    }
}
