using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected SpriteRenderer weaponRenderer;
    [SerializeField] protected bool isRotate;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackDelay;
    protected bool isUseable;

    protected Item weapon;
    protected IAttackStat owner;
    protected LayerMask targetMask;

    [SerializeField]protected List<GameObject> targetList = new List<GameObject>();

    public virtual void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask)
    {
        this.weapon = weapon;
        animator = GetComponent<Animator>();
        isUseable = true;
        owner = ownerStat;
        this.targetMask = targetMask;
    }

    public virtual void Attack()
    {
        if (isUseable)
        {
            isUseable = false;
            Invoke("ApplyDelay", attackDelay);
            AttackAnimation();
            AttackAction();
        }
    }

    public virtual void AttackAction() { }

    public virtual void AttackAnimation() 
    {
        animator.SetTrigger("Attack");
    }

    public virtual void ApplyDelay()
    {
        isUseable = true;
    }

    public virtual void Rotate(float angle) { }
}


