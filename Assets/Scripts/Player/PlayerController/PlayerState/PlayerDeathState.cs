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

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Death);
        player.PlalyerDeathParticle.Play();
        player.WeaponHandler.OwnerDeath();
    }
}
