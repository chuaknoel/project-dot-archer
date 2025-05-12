using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStat
{
    float AttackDamage { get; }
    float BuffDamage { get; }

    public void AddDamage(float addDamage)
    {

    }

    public float GetTotalAttack()
    {
        return 0;
    }
}
