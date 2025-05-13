using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : BaseStat ,  IAttackStat, IMoveStat
{
    public float MoveSpeed => moveSpeed;
    public float AttackDamage => attackDamage;

    public float BuffDamage => buffDamage;

    public float BuffSpeed => buffSpeed;

    private float buffDamage;
    private float buffSpeed;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;

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
