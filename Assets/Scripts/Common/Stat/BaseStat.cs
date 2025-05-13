using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStat : MonoBehaviour
{
    [SerializeField] protected int level;
    public int Level { get { return level; } }

    protected int exp;
    public int Exp { get { return exp; } }

    [SerializeField] protected float maxHealth;
    public float MaxHealth { get { return maxHealth; } }

    [SerializeField] protected float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }

    public bool IsDeath { get { return CurrentHealth <= 0; } }

    public virtual void Death()
    {

    }
}
