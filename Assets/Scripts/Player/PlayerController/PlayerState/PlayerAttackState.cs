using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStates
{
    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Attack;
    }

    public override void OnEnter()
    {
        

        base.OnEnter();
        player.ChangeAnime(PlayerState.Attack);
        player.Attack();
        //attackTime
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        //if(elapsedTime >= attackTime ) { Idle }
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
