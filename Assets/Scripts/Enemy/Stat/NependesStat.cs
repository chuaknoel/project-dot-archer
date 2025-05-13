using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NependesStat : EnemyStat , IAttackStat
{
    [SerializeField] private float attackDamage;
    public float AttackDamage => attackDamage;

    private float buffDamage;
    public float BuffDamage => buffDamage;

    public void AddDamage(float addDamage) // 1.5¹è¸é addDamage = 0.5
    {
        buffDamage = addDamage;
    }

    public float GetTotalDamage()
    {
        return (1 + buffDamage) * attackDamage;
    }
}
