using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : EnemyStat, IAttackStat, IMoveStat
{
    public float MoveSpeed => moveSpeed;
    public float AttackDamage => attackDamage;
    public float GetTotalStatDamage => GetTotalStatDamage;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;


    public override void TakeDamage(float damage)
    {
        Debug.Log("ÇÇ°Ý");
       base.TakeDamage(damage);
        Debug.Log(currentHealth);
    }
    protected override void UpdateHp()
    {
        BossHpBarController hpBarController = GetComponent<BossHpBarController>();
        hpBarController.UpdateHP(CurrentHealth, MaxHealth);
    }

    float IAttackStat.GetTotalStatDamage()
    {
        return attackDamage;
    }
}
