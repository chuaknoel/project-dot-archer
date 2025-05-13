using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcStat : EnemyStat,IAttackStat
{
    public float AttackDamage => attackDamage;
    [SerializeField] private float attackDamage;

    public float GetTotalStatDamage()
    {
        return attackDamage;
    }
}
