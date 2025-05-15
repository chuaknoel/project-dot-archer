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
         animator =transform.GetComponentInChildren<Animator>();
    }

    public virtual void TakeDamage(float damage)
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
        Inventory inventory = GameObject.Find("Inventory")?.GetComponent<Inventory>();

        if (inventory != null)
        {
            inventory.AddGold(1);
            Destroy(hpBarController.hpBar);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogWarning("Inventory 오브젝트를 찾을 수 없거나 Inventory 컴포넌트가 없습니다.");
        }
    }
}
    
