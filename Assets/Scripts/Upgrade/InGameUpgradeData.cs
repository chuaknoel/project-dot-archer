using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class InGameUpgradeData : MonoBehaviour
{
    public AttackTpye attackType;

    public int addProjectileCount;
    public float addProjectileSpeed;

    public int addBurstCount;

    public float addWeaponDamage;

    public float addAttackCooldown;
    public float addAttackDelay;

    public int addReflection;

    //업그레이드 카드를 만들고 카드별로 업그레이 될 정보만 수정해서 카드 선택시 Upgrade 함수를 통해 플레이어에게 전달
    public void Upgrade()
    {
        DungeonManager.Instance.player.Upgrade(this);
    }

    public void ResetUpgrade() 
    {
        addProjectileCount = 0;
        addProjectileSpeed = 0;

        addBurstCount = 0;

        addWeaponDamage = 0;

        addAttackCooldown = 0;
        addAttackDelay = 0;

        addReflection = 0;
    }
}
