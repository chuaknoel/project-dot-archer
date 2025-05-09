using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackController : MonoBehaviour
{
    private bool canMove = true;


    public void RangeAttack(float damage, IAttackRangeStat range, BaseEnemy ownerEnemy)
    {
        if (canMove)
        {
            StartCoroutine(RangeAttackRoutine(damage, range, ownerEnemy));
        }
    }
    public IEnumerator RangeAttackRoutine(float damage, IAttackRangeStat range, BaseEnemy ownerEnemy)
    {

        canMove = false;

        yield return new WaitForSeconds(0.5f);

        GameObject projectile = Instantiate(range.ProjectilePrefab, transform.position, Quaternion.identity);

        Vector3 direction = (ownerEnemy.target.position - transform.position).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * range.ProjectileSpeed;
        }

        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
}
