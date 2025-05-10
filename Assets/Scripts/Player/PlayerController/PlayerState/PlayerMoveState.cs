using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMoveState : PlayerStates
{
    private Vector3 inputDir;

    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Move;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Move);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        inputDir = player.Controller.GetInputDir();

        if (inputDir == Vector3.zero)
        {
            player.Controller.ChangeState(nameof(PlayerIdleState));
        }
    }

    public override void OnFixedUpdate()
    {
        player.transform.position += inputDir.normalized * player.stat.MoveSpeed * Time.fixedDeltaTime;
    }


    public override void OnExit()
    {
        base.OnExit();
    }
}
