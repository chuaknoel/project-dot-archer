using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerStates
{
    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Action;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Action);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        
    }
}
