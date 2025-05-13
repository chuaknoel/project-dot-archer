using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUpgradeData : MonoBehaviour
{
    public AttackTpye attackType;

    public int addProjectile;
    public float addProjectileSpeed;

    public int addDamage;
    public int addAttackSpeed;
    public int addAttackDelay;

    public int addReflict;

    //업그레이드 카드를 만들고 카드별로 업그레이 될 정보만 수정해서 카드 선택시 Upgrade 함수를 통해 플레이어에게 전달
    public void Upgrade()
    {
        DungeonManager.Instance.player.Upgrade(this);
    }
}
