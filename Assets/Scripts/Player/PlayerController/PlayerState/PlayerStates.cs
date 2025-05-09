using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : State<Player>
{
    protected PlayerState state;
    protected Player player;


    public override void Init(Player player)
    {
        this.player = player;
    }

    public PlayerState GetState()
    {
        return state;
    }
}
