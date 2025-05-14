using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcStat : EnemyStat,IAttackStat, IMoveStat
{
    public float MoveSpeed => moveSpeed;
    public float AttackDamage => attackDamage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;

    public float GetTotalStatDamage()
    {
        return attackDamage;
    }
}
