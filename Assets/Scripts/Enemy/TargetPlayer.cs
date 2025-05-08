using System.Collections;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float detectionDistance = 1f;
    public LayerMask obstacleLayer;

    private bool isAvoiding = false; // 회피 중인지 여부

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isAvoiding)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (target == null) return;

        Vector3 moveDir = (target.position - transform.position).normalized;
        transform.localScale = new Vector3(target.position.x < transform.position.x ? -1 : 1, 1, 1);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, detectionDistance, obstacleLayer);
        Debug.DrawRay(transform.position, moveDir * detectionDistance, Color.red);

        if (hit.collider != null)
        {
            StartCoroutine(AvoidObstacle(moveDir)); // 회피 코루틴 실행
        }
        else
        {
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }

    IEnumerator AvoidObstacle(Vector3 originalDir)
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
