using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZombieStat : EnemyStat, IMoveStat, IAttackStat , IAttackRangeStat
{
    public float MoveSpeed => moveSpeed;

    public float AttackDamage => attackDamage;

    public float AttackRange => attakRange;

    public float AttackDelay => attakDelay;

    public float ProjectileSpeed => projectileSpeed;

    public GameObject ProjectilePrefab => projectilePrefab;


    public GameObject projectilePrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attakRange;
    [SerializeField] private float attakDelay;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float attackDelay;
}
