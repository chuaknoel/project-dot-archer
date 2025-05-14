using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerJumpState : PlayerStates
{
    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Jump;
    }
}
