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

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(player.GetInputDir() != Vector3.zero)
        {
            player.controller.ChangeState(nameof(PlayerMoveState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
