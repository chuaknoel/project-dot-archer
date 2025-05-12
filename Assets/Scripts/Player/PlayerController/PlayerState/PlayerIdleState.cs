using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStates
{
    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Idle);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if(player.Controller.GetInputDir() != Vector3.zero)
        {
            player.Controller.ChangeState(nameof(PlayerMoveState));
        }

        if (player.SearchTarget.SearchNearestTarget() != null)
        {
            if (player.WeaponHandler.IsUseable())
            {
                player.Controller.ChangeState(nameof(PlayerAttackState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
