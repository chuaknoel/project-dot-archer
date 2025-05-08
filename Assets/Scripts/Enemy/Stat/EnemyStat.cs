using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat, IDefenceStat
{
    [SerializeField] private float defence;
    public float Defence => defence;


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
    
