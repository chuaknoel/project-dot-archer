using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projectileName;
    public int MaxSize;
    public float duration;
    public float projectileSpeed;

    private RangeWeaponHandler rangeWeaponHandler;
    private Vector2 direction;

    private bool isActive;
    private float currentDuration;
    private Rigidbody2D proejectileRigidbody;

    private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    public void Init()
    {
        isActive = false;
        currentDuration = 0f;
        proejectileRigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetProjectile(RangeWeaponHandler rangeWeaponHandler, Transform pivot, LayerMask targetMask)
    {
        this.rangeWeaponHandler = rangeWeaponHandler;

        this.targetMask = targetMask;

        transform.position = pivot.position;
        transform.rotation = pivot.rotation;

        direction = transform.right;

        isActive = true;
        currentDuration = 0;
    }

    private void Update()
    {
        if (!isActive) return;

        currentDuration += Time.deltaTime;

        if (currentDuration >= duration)
        {
            rangeWeaponHandler.connectedPool.Release(this);
            return;
        }

        proejectileRigidbody.velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((targetMask & (1 << collision.gameObject.layer)) != 0)
        {
            if(collision.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage(rangeWeaponHandler.GetAttackDamage());
                Debug.Log($"{collision.gameObject.name} 에게 {rangeWeaponHandler.GetAttackDamage()} 데미지!");
            }
            rangeWeaponHandler.connectedPool.Release(this);
        }

        if((obstacleMask & (1 << collision.gameObject.layer)) != 0)
        {
            rangeWeaponHandler.connectedPool.Release(this);
        }
    }
}
