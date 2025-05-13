using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperStat : EnemyStat, IAttackStat
{
    public float AttackDamage => attackDamage;
    [SerializeField] private float attackDamage;

    public float GetTotalStatDamage()
    {
        return attackDamage;
    }
}
