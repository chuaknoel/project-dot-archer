using System.Collections;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    public Transform target;
    public float detectionDistance = 1f;
    public LayerMask obstacleLayer;
    public float MoveSpeed = 2f;
    private bool isAvoiding = false;

    public void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FollowPlayer()
    {
        if (!isAvoiding)
        {
            if (target == null) return;

            Vector3 moveDir = (target.position - transform.position).normalized;
            transform.localScale = new Vector3(target.position.x < transform.position.x ? -1 : 1, 1, 1);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, detectionDistance, obstacleLayer);
            Debug.DrawRay(transform.position, moveDir * detectionDistance, Color.red);

            if (hit.collider != null)
            {
                StartCoroutine(AvoidObstacle(moveDir));
            }
            else
            {
                transform.position += moveDir * MoveSpeed * Time.deltaTime;
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
