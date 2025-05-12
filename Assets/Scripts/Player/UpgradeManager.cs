using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public InGameUpgradeData rangeUpgradeData = new();
    public InGameUpgradeData meleeUpgradeData = new();

    public PermanentUpgradeData permanentUpgradeData = new();

    public void MergeUpgrade(InGameUpgradeData addUpgrade)
    {
        if (addUpgrade.attackType == AttackTpye.Range)
        {
            RangeUpgrade(addUpgrade);
        }
        else if(addUpgrade.attackType == AttackTpye.Melee)
        {
            MeleeUpgrade(addUpgrade);
        }
    }

    public void RangeUpgrade(InGameUpgradeData addupgrade)
    {
        rangeUpgradeData.addProjectile += addupgrade.addProjectile;
        rangeUpgradeData.addProjectileSpeed += addupgrade.addProjectileSpeed;

        rangeUpgradeData.addWeaponDamage += addupgrade.addWeaponDamage;
        rangeUpgradeData.addAttackCooldown += addupgrade.addAttackCooldown;
        rangeUpgradeData.addAttackDelay += addupgrade.addAttackDelay;

        rangeUpgradeData.addReflection += addupgrade.addReflection;
    }

    public void MeleeUpgrade(InGameUpgradeData addupgrade)
    {
        meleeUpgradeData.addProjectile += addupgrade.addProjectile;
        meleeUpgradeData.addProjectileSpeed += addupgrade.addProjectileSpeed;

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

    public void ResetUpgrade()
    {
        rangeUpgradeData = new();
        meleeUpgradeData = new();
    }
}
