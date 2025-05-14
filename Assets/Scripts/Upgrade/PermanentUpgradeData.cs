using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgradeData : MonoBehaviour
{
    private float AttackDamage;
    private float Defence;
    private float MoveSpeed;

    public void LoadData(PermanentUpgradeData upgradeData)
    {
        AttackDamage = upgradeData.AttackDamage;
        Defence = upgradeData.Defence;
        MoveSpeed = upgradeData.MoveSpeed;
    }

    public void UpgradeAttackDamage(float amount)
    {
        AttackDamage += amount;
    }

    public void UpgradeDefence(float amount)
    {
        Defence += amount;
    }

    public void UpgradeMoveSpeed(float amout)
    {
        MoveSpeed += amout;
    }

    public float GetAttackDamage()
    {
        return AttackDamage;
    }
    public float GetDefence()
    {
        return Defence;
    }
    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }
}