using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float detectionDistance = 1f;
    public LayerMask obstacleLayer = 1 << 4;
    private bool isAvoiding = false;

    public void MoveToPlayer(Transform target, IMoveStat move)
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
                StartCoroutine(AvoidObstacle(moveDir, move.MoveSpeed));
            }
            else
            {
                transform.position += moveDir * move.MoveSpeed * Time.deltaTime;
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
}
