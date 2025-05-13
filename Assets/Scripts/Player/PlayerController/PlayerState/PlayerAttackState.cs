using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class PlayerAttackState : PlayerStates
{
    public override void Init(Player player)
    {
        base.Init(player);
        state = PlayerState.Attack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.ChangeAnime(PlayerState.Attack);
        player.WeaponHandler.Attack();
    }
       
    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        //if(elapsedTime >= 공격 딜레이시간) AttackState에서 경과한 시간이 무기 공격 후 후딜레이가 완료되서 움직일 수 있는 상태가 되면 IdleState호출
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
