using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected SpriteRenderer weaponRenderer;
    [SerializeField] protected bool isRotate;
  
    protected bool isUseable;

    protected Item weapon;
    protected IAttackStat ownerStat;
    protected LayerMask targetMask;

    protected SpriteRenderer waeponSprite;

    public virtual void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask)
    {
        this.weapon = weapon;
        animator = GetComponent<Animator>();
        isUseable = true;
        this.ownerStat = ownerStat;
        this.targetMask = targetMask;
        waeponSprite ??= GetComponentInChildren<SpriteRenderer>();
        waeponSprite.sprite = weapon.ItemData.itemIcon;
    }

    public virtual void Attack()
    {
        ResetCooldown();
        AttackAnimation();
        AttackAction();
    }

    public virtual void AttackAction() { }

    public virtual void AttackAnimation() 
    {
        animator.SetTrigger("Attack");
    }

    public void ResetCooldown()
    {
        StartCoroutine(eResetCooldown());
    }

    IEnumerator eResetCooldown()
    {
        isUseable = false;

        yield return new WaitForSeconds(weapon.ItemData.attackCooldown);

        isUseable = true;
    }

    public virtual void Rotate(float angle) { }

    public bool IsUseable()
    {
        return isUseable;
    }

    public float GetWeaponDelay()
    {
        return weapon.ItemData.attackDelay;
    }
}


