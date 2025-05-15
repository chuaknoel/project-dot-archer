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

    BossHpBarController hpBarController;
    public override void TakeDamage(float damage)
    {
        if (IsDeath) return;
        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage);
        currentHealth -= applyDamage;

        // HP Bar 업데이트
        UpdateHp();
        // 피격 애니메이션 재생
        PlayAnimation();

        if (IsDeath)
        {
            Death();
        }

    }
    protected override void UpdateHp()
    {
        hpBarController = GetComponent<BossHpBarController>();
        hpBarController.UpdateHP(CurrentHealth, MaxHealth);
    }
    protected override void PlayAnimation()
    {
        animator.SetBool("isDamaged", true);
        Invoke("EndDamaged", 0.15f);

    }
     float IAttackStat.GetTotalStatDamage()
    {
        return attackDamage;
    }
    public override void Death()
    {
        base.Death();
    }
}
