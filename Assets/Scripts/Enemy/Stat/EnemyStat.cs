using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat ,IDefenceStat
{
    public float Defence => defence;
    protected float defence;
    Animator animator;

    void Start()
    {
         animator =transform.GetChild(0).GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDeath) return;
        Debug.Log("피격2");
        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage - damage);
        currentHealth -= applyDamage;
        Debug.Log(currentHealth);
        
        // HP Bar 업데이트
        UpdateHp();
      
        animator.SetBool("isDamaged",true);
        Invoke("EndDamaged", 1f);
         
        if (IsDeath)
        {
            Death();
        }
    }
    protected virtual void UpdateHp()
    {
        HpBarController hpBarController = GetComponent<HpBarController>();
        hpBarController.UpdateHP(CurrentHealth, MaxHealth);
    }
    void EndDamaged()
    {
        animator.SetBool("isDamaged", false);
    }
    //public override void TakeDamage(float damage)
    //{
    //    if (IsDeath) return;

    //    float applyDamage = Mathf.Clamp(damage - Defence, 0, damage - damage);
    //    currentHealth -= applyDamage;

    //    if (IsDeath)
    //    {
    //        Death();
    //    }
    //}
}
    
