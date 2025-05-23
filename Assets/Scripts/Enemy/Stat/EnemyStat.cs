using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat ,IDefenceStat
{
    public float Defence => defence;
    protected float defence;
    protected Animator animator;
    HpBarController hpBarController;
    void Start()
    {
         animator = transform.GetComponentInChildren<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDeath) return;

      //  float applyDamage = Mathf.Clamp(damage - Defence, 0, damage-damage);
        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage);
        currentHealth -= applyDamage;

        // HP Bar 업데이트
        UpdateHp();
        // 피격 애니메이션 재생
        PlayAnimation();

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    protected virtual void UpdateHp()
    {
        hpBarController = GetComponent<HpBarController>();
        hpBarController.UpdateHP(CurrentHealth, MaxHealth);
    }

    protected virtual void PlayAnimation()
    {
        animator.SetBool("isDamaged", true);
        Invoke("EndDamaged", 0.15f);

    }
    void EndDamaged()
    {
        animator.SetBool("isDamaged", false);
    }
    public override void Death()
    {
        if (!IsDeath)
        {
            IsDeath = true;
            GameManager.Instance.inventory.AddGold(1);                                               // 코인 증가
            DungeonManager.Instance.enemyManager.OnEnemyDefeated(this.GetComponent<BaseEnemy>());    // 에너미 죽음 체크
            Destroy(hpBarController.hpBar);
            Destroy(this.gameObject);
        }
    }
}
    
