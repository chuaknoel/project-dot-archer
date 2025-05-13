using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : EnemyStat ,  IAttackStat, IMoveStat
{
    public float AttackDamage => attackDamage;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private float attackDamage;
    [SerializeField] private float moveSpeed;

    public GoblinStat(float attackDamage, float moveSpeed, float defence)
    {
        this.attackDamage = attackDamage;
        this.moveSpeed = moveSpeed;
        this.defence = defence;
    }

    public float GetTotalStatDamage()
    {
        return attackDamage;
    }

}
