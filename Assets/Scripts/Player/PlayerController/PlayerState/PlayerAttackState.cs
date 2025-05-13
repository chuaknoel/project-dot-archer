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
        //if(elapsedTime >= ���� �����̽ð�) AttackState���� ����� �ð��� ���� ���� �� �ĵ����̰� �Ϸ�Ǽ� ������ �� �ִ� ���°� �Ǹ� IdleStateȣ��
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
