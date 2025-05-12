using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NependesStat : EnemyStat , IAttackStat
{
    public float AttackDamage => attackDamage;

    [SerializeField] private float attackDamage;
}
