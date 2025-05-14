using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class EnemyMoveState : EnemyStates
{
    private float detectionDistance = 1f;
    private bool isAvoiding = false;

    public override void Init(BaseEnemy enemy)
    {
        base.Init(enemy);
        state = EnemyState.Move;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //enemy.ChangeAnime(EnemyState.Move);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        MoveToPlayer();
        if (enemy.currentSkill.CanUse())
        {
            enemy.UseSkill();
        }

    }

    public override void OnFixedUpdate()
    {

    }
    public void MoveToPlayer()
    {
        if (!isAvoiding)
        {
            if (enemy.target == null) return;

            Vector3 moveDir = (enemy.target.position - enemy.transform.position).normalized;
            enemy.transform.localScale = new Vector3(enemy.target.position.x < enemy.transform.position.x ? -1 : 1, 1, 1);

            RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, moveDir, detectionDistance, enemy.obstacleLayer);
            Debug.DrawRay(enemy.transform.position, moveDir * detectionDistance, Color.red);

            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                AvoidObstacle(enemy.transform.position);
            }
            else
            {
                enemy.transform.position += moveDir * enemy.enemyStat.MoveSpeed * Time.deltaTime;
            }

        }
    }

    public void AvoidObstacle(Vector3 originalDir)
    {
        isAvoiding = true;

        Vector3 avoidDir = Vector2.Perpendicular(originalDir).normalized;

        float timer = 0f;
        while (timer < 0.8f)
        {
            enemy.transform.position += avoidDir * enemy.enemyStat.MoveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            return;
        }

        isAvoiding = false;
    }
}
