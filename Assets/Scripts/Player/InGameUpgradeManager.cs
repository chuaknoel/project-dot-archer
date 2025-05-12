using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUpgradeManager : MonoBehaviour
{
    public InGameUpgradeData rangeUpgradeData = new();
    public InGameUpgradeData meleeUpgradeData = new();

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

        rangeUpgradeData.addDamage += addupgrade.addDamage;
        rangeUpgradeData.addAttackSpeed += addupgrade.addAttackSpeed;
        rangeUpgradeData.addAttackDelay += addupgrade.addAttackDelay;

        rangeUpgradeData.addReflict += addupgrade.addReflict;
    }

    public void MeleeUpgrade(InGameUpgradeData addupgrade)
    {
        meleeUpgradeData.addProjectile += addupgrade.addProjectile;
        meleeUpgradeData.addProjectileSpeed += addupgrade.addProjectileSpeed;

        meleeUpgradeData.addDamage += addupgrade.addDamage;
        meleeUpgradeData.addAttackSpeed += addupgrade.addAttackSpeed;
        meleeUpgradeData.addAttackDelay += addupgrade.addAttackDelay;

        meleeUpgradeData.addReflict += addupgrade.addReflict;
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
