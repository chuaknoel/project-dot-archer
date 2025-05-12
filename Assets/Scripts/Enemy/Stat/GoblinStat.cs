using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : BaseStat ,  IAttackStat, IMoveStat
{
    public float AttackDamage => attackDamage;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private float attackDamage;
    [SerializeField] private float moveSpeed;

    public float GetTotalStatDamage()
    {
        return attackDamage;
    }

}
