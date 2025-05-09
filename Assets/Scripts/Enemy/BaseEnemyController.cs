using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    public float detectionDistance = 1f;
    public LayerMask obstacleLayer = 1 << 4; // 레이어 마스크 설정
    public float MoveSpeed = 2f;
    private bool isAvoiding = false;
    private BaseEnemy ownerEnemy;
    private BaseStat ownerStat;

    private void Update()
    {
        if ((ownerEnemy.target.transform.position - transform.position).magnitude < 1.0f)
        {
            if (ownerStat is IAttackStat attackStat)
            {
                Attack();
            }
        }
        else
        {
            if (ownerStat is IMoveStat moveStat)
            {
                MoveToPlayer(ownerEnemy.target, moveStat.MoveSpeed);
            }
        }
    }

    public void Attack()
    {

    }

    public void Init(BaseEnemy ownerEnemy)
    {
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
    }

    public void MoveToPlayer(Transform target,float speed)
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
                StartCoroutine(AvoidObstacle(moveDir));
            }
            else
            {
                transform.position += moveDir * speed * Time.deltaTime;
            }

        }
    }

    IEnumerator AvoidObstacle(Vector3 originalDir)
    {
        isAvoiding = true;

        Vector3 avoidDir = Vector2.Perpendicular(originalDir).normalized;

        float timer = 0f;
        while (timer < 0.8f)
        {
            transform.position += avoidDir * MoveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        isAvoiding = false;
    }
}
