using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat, IAttackStat, IDefenceStat, IMoveStat
{
    [SerializeField] private float attackDamage;
    public float AttackDamage => attackDamage;

    [SerializeField] private float defence;
    public float Defence => defence;

    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    public void TakeDamage(float damage)
    {
        if(IsDeath) return;

        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage - damage);
        currentHealth -= applyDamage;

        if (IsDeath)
        {
            Death();
        }
    }
}
