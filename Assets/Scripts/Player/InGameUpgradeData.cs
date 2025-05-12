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

    //���׷��̵� ī�带 ����� ī�庰�� ���׷��� �� ������ �����ؼ� ī�� ���ý� Upgrade �Լ��� ���� �÷��̾�� ����
    public void Upgrade()
    {
        DungeonManager.Instance.player.Upgrade(this);
    }
}
