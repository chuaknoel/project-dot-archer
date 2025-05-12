using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackController : MonoBehaviour
{
    public bool isAttacking = true;
    public bool canMove = true;

    public void RangeAttack(float damage, IAttackRangeStat range, BaseEnemy ownerEnemy)
    {
        if (!isAttacking) return;
        StartCoroutine(RangeAttackRoutine(damage, range, ownerEnemy));
    }
    public IEnumerator RangeAttackRoutine(float damage, IAttackRangeStat range, BaseEnemy ownerEnemy)
    {
        isAttacking = false;

        yield return new WaitForSeconds(range.AttackDelay);

        canMove = false;

        yield return new WaitForSeconds(range.AttackDelay);

        GameObject projectile = Instantiate(range.ProjectilePrefab, transform.position, Quaternion.identity);

        Vector3 direction = (ownerEnemy.target.position - transform.position).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * range.ProjectileSpeed;
        }



        canMove = true;
        isAttacking = true;

    }

}
