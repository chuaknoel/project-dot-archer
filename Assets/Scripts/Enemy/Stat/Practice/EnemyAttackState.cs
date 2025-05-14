using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class EnemyAttackState : EnemyStates
{
    public override void Init(BaseEnemy enemy)
    {
        base.Init(enemy);
        state = EnemyState.Attack;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        //enemy.ChangeAnime(EnemyState.Attack);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if (elapsedTime > enemy.enemyStat.AttackDelay)
            enemy.Controller.ChangeState(nameof(EnemyMoveState));
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}

