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
        player.WeaponHandler.Attack();        
    }
       
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        
        if(elapsedTime >= player.WeaponHandler.GetWeaponDelay())
        {
            player.Controller.ChangeState(nameof(PlayerIdleState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
