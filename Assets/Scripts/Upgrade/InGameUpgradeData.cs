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

    //���׷��̵� ī�带 ����� ī�庰�� ���׷��� �� ������ �����ؼ� ī�� ���ý� Upgrade �Լ��� ���� �÷��̾�� ����
    public void Upgrade()
    {
        DungeonManager.Instance.player.Upgrade(this);
    }
}
