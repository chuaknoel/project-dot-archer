using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController<Player>
{
    protected Player player;

    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
    }
}
