using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat ,IDefenceStat , IMoveStat
{
    public float Defence => defence;
    [SerializeField] protected float defence;

    public float MoveSpeed => moveSpeed;
    [SerializeField] protected float moveSpeed;
    

    public void TakeDamage(float damage)
    {
        if (IsDeath) return;

        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage - damage);
        currentHealth -= applyDamage;

        if (IsDeath)
        {
            Death();
        }
    }
}
    
