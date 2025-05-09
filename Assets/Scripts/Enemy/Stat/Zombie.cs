using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZombieStat : EnemyStat, IMoveStat, IAttackStat
{
    public float MoveSpeed => moveSpeed;

    public float AttackDamage => attackDamage;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
}
