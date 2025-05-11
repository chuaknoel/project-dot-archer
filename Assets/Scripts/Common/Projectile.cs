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

    public void Init()
    {
        isActive = false;
        duration = 0f;
        proejectileRigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetProjectile(RangeWeaponHandler rangeWeaponHandler, Transform pivot)
    {
        this.rangeWeaponHandler = rangeWeaponHandler;

        transform.position = pivot.position;
        transform.rotation = pivot.rotation;

        direction = transform.right;

        isActive = true;
        currentDuration = 0;
    }

    private void Update()
    {
        //if (!isActive) return;

        /*
        currentDuration += Time.deltaTime;

        if (currentDuration >= duration)
        {
            rangeWeaponHandler.connectedPool.Release(this);
            return;
        }
        */

        proejectileRigidbody.velocity = direction * projectileSpeed;
    }
}
