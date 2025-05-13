using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZombieStat : EnemyStat, IMoveStat, IAttackStat
{
    [SerializeField] private float attackDamage;
    public float AttackDamage => attackDamage;

    private float buffDamage;
    public float BuffDamage => buffDamage;


    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    private float buffSpeed;
    public float BuffSpeed => buffSpeed;

    public void AddDamage(float addDamage) // 1.5배면 addDamage = 0.5
    {
        buffDamage = addDamage;
    }

    public float GetTotalDamage()
    {
        return ((1 + buffDamage) * attackDamage);
    }

    public void AddSpeed(float addSpeed) // 1.5배면 addSpeed = 0.5
    {
        buffSpeed = addSpeed;
    }

    public float GetTotalSpeed()
    {
        return (1 + buffSpeed) * moveSpeed;
    }
}