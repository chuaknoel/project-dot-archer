using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerDeathState : PlayerStates
{
    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Death;
    }
}
