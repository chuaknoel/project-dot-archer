using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class UpgradeManager : MonoBehaviour
{
    public InGameUpgradeData rangeUpgradeData;
    public InGameUpgradeData meleeUpgradeData;

    public PermanentUpgradeData permanentUpgradeData = new();

    public void MergeUpgrade(InGameUpgradeData addUpgrade)
    {
        if (addUpgrade.attackType == AttackTpye.Range)
        {
            RangeUpgrade(addUpgrade);
        }
        else if (addUpgrade.attackType == AttackTpye.Melee)
        {
            MeleeUpgrade(addUpgrade);
        }
    }

    public void RangeUpgrade(InGameUpgradeData addupgrade)
    {
        rangeUpgradeData.addProjectileCount += addupgrade.addProjectileCount;
        rangeUpgradeData.addProjectileSpeed += addupgrade.addProjectileSpeed;

        rangeUpgradeData.addBurstCount += addupgrade.addBurstCount;

        rangeUpgradeData.addWeaponDamage += addupgrade.addWeaponDamage;
        rangeUpgradeData.addAttackCooldown += addupgrade.addAttackCooldown;
        rangeUpgradeData.addAttackDelay += addupgrade.addAttackDelay;

        rangeUpgradeData.addReflection += addupgrade.addReflection;
    }

    public void MeleeUpgrade(InGameUpgradeData addupgrade)
    {
        meleeUpgradeData.addProjectileCount += addupgrade.addProjectileCount;
        meleeUpgradeData.addProjectileSpeed += addupgrade.addProjectileSpeed;

        meleeUpgradeData.addBurstCount -= addupgrade.addBurstCount;

        meleeUpgradeData.addWeaponDamage += addupgrade.addWeaponDamage;
        meleeUpgradeData.addAttackCooldown += addupgrade.addAttackCooldown;
        meleeUpgradeData.addAttackDelay += addupgrade.addAttackDelay;

        meleeUpgradeData.addReflection += addupgrade.addReflection;
    }

    public InGameUpgradeData GetRangeUpgrade()
    {
        return rangeUpgradeData;
    }

    public InGameUpgradeData GetMeleeUpgrade()
    {
        return meleeUpgradeData;
    }

    public void Init()
    {
        rangeUpgradeData = new();
        meleeUpgradeData = new();
    }
}
