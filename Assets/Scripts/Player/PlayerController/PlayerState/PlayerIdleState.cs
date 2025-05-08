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
        player.ChangeAnime(PlayerState.Idle);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if(player.GetInputDir() != Vector3.zero)
        {
            player.Controller.ChangeState(nameof(PlayerMoveState));
        }

        if(player.SearchTarget.SearchNearestTarget() != null)
        {
            player.Controller.ChangeState(nameof(PlayerAttackState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
