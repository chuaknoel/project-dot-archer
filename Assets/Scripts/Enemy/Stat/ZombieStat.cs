using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZombieStat : EnemyStat, IAttackStat
{

    public float AttackDamage => attackDamage;


    [SerializeField] private float attackDamage;

    public float GetTotalStatDamage()
    {
        return attackDamage;
    }
}
    
