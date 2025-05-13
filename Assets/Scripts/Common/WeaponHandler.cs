using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected SpriteRenderer weaponRenderer;
    [SerializeField] private ParticleSystem ownerDeathParticle;

    protected bool isUseable;

    protected Item weapon;
    protected IAttackStat ownerStat;
    protected Collider2D ownerCollider;

    protected LayerMask targetMask;

    protected SpriteRenderer waeponSprite;

    public virtual void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask, Collider2D ownerColler)
    {
        this.weapon = weapon;
        animator = GetComponent<Animator>();
        isUseable = true;
        this.ownerStat = ownerStat;
        this.targetMask = targetMask;
        waeponSprite ??= GetComponentInChildren<SpriteRenderer>();
        waeponSprite.sprite = weapon.ItemData.itemIcon;
        this.ownerCollider = ownerColler;
    }

    public virtual void Attack()
    {
        ResetCooldown();
        AttackAnimation();
        AttackAction();
    }

    public virtual float GetAttackDamage()
    {
        return ownerStat.GetTotalStatDamage();
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

        yield return new WaitForSeconds(GetWeaponCooldown());

        isUseable = true;
    }

    public virtual void Rotate(Vector3 angle) { }

    public bool IsUseable()
    {
        return isUseable;
    }

    public virtual float GetWeaponDelay()
    {
        return weapon.ItemData.attackDelay;
    }

    public virtual float GetWeaponCooldown()
    {
        return weapon.ItemData.attackCooldown;
    }

    public void OwnerDeath()
    {
        animator.SetTrigger("OwnerDeath");
        ownerDeathParticle.transform.rotation = Quaternion.Euler(-100f, 0, 0);
        ownerDeathParticle.Play();
    }
}


