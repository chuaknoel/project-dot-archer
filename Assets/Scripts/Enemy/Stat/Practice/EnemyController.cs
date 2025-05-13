using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class EnemyController : BaseController<BaseEnemy>
{
    
    private float detectionDistance = 1f;
    public LayerMask obstacleLayer = 1 << 4;
    private bool isAvoiding = false;

    protected BaseEnemy enemy;
    protected EnemyStat enemyStat;
    private Action action;



    
    public EnemyController(State<BaseEnemy> initState, BaseEnemy enemy) : base(initState, enemy)
    {
        this.enemy = enemy;
    }

    public override void OnUpdate(float deltaTime)
    {
        currentState.OnUpdate(deltaTime);
        base.OnUpdate(deltaTime);
    }

    public void MoveToPlayer(Transform target, float speed)
    {
        if (!isAvoiding)
        {
            if (target == null) return;

            Vector3 moveDir = (target.position - enemy.transform.position).normalized;
            enemy.transform.localScale = new Vector3(target.position.x < enemy.transform.position.x ? -1 : 1, 1, 1);

            RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, moveDir, detectionDistance, obstacleLayer);
            Debug.DrawRay(enemy.transform.position, moveDir * detectionDistance, Color.red);

            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                AvoidObstacle(enemy.transform.position, speed);
            }
            else
            {
                enemy.transform.position += moveDir * speed * Time.deltaTime;
            }

        }
    }

    public void  AvoidObstacle(Vector3 originalDir, float speed)
    {
        isAvoiding = true;

        Vector3 avoidDir = Vector2.Perpendicular(originalDir).normalized;

        float timer = 0f;
        while (timer < 0.8f)
        {
            enemy.transform.position += avoidDir * speed * Time.deltaTime;
            timer += Time.deltaTime;
            return;
        }

        isAvoiding = false;
    }

}

