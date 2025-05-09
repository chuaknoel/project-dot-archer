using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    public float detectionDistance = 1f;
    public LayerMask obstacleLayer = 1 << 4; // 레이어 마스크 설정
    private bool isAvoiding = false;
    private BaseEnemy ownerEnemy;
    private BaseStat ownerStat;
    private float attackTimer = 0f;
    private bool canMove = true;
    private float time;

    private void Update()
    {

        if (ownerStat is IAttackRangeStat range && ownerStat is IAttackStat attack)
        {
            if (attackTimer >= range.AttackDelay)
            {
                RangeAttack(attack.AttackDamage);
                attackTimer = 0f;
            }

            else
            {
                if (ownerStat is IMoveStat moveStat && canMove)
                {
                    MoveToPlayer(ownerEnemy.target, moveStat.MoveSpeed);
                }
            }
            attackTimer += Time.deltaTime;
        }

        else
        {
            if (ownerStat is IMoveStat moveStat)
            {
                MoveToPlayer(ownerEnemy.target, moveStat.MoveSpeed);
            }
        }
    }
    private void RangeAttack(float damage)
    {
        if (ownerStat is IAttackRangeStat range)
        {
            StartCoroutine(RangeAttackRoutine(damage, range));
        }

    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage((ownerStat as IAttackStat).AttackDamage);
            }
        }
    }

    public void Init(BaseEnemy ownerEnemy)
    {
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
    }

    public void MoveToPlayer(Transform target, float speed)
    {

        if (!isAvoiding)
        {
            if (target == null) return;

            Vector3 moveDir = (target.position - transform.position).normalized;
            transform.localScale = new Vector3(target.position.x < transform.position.x ? -1 : 1, 1, 1);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, detectionDistance, obstacleLayer);
            Debug.DrawRay(transform.position, moveDir * detectionDistance, Color.red);

            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                StartCoroutine(AvoidObstacle(moveDir, speed));
            }
            else
            {
                transform.position += moveDir * speed * Time.deltaTime;
            }

        }
    }

    IEnumerator AvoidObstacle(Vector3 originalDir, float speed)
    {
        isAvoiding = true;

        Vector3 avoidDir = Vector2.Perpendicular(originalDir).normalized;

        float timer = 0f;
        while (timer < 0.8f)
        {
            transform.position += avoidDir * speed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        isAvoiding = false;
    }

    IEnumerator CanMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true; // 이동 중지

    }

    private IEnumerator RangeAttackRoutine(float damage, IAttackRangeStat range)
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
