using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : BaseController<Player>
{
    protected Player player;
    private Vector3 inputDir;

    private Action lookAction;

    private bool enemyAlive;

    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
        lookAction = LookEnemy;
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();
        lookAction?.Invoke();
        base.OnUpdate(deltaTime);
    }

    public Vector3 GetInputDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        return inputDir;
    }

    public void ChangeLook()
    {
        lookAction = null;

        if (enemyAlive) //임시로 만든 bool 변수 //추후 EnemyManager에서 남은 Enemy를 체크하여 변경
        {
            lookAction = LookEnemy;
        }
        else
        {
            lookAction += LookMouse;
        }
    }

    public void LookMouse()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = worldPos - (Vector2)player.transform.position;
        RotateToDir(lookDir);
        //player.WeaponHandler?.Rotate(rotZ);
    }

    public void LookEnemy()
    {
        GameObject target = player.SearchTarget.SearchNearestTarget();

        if (target == null)
        {
            return;
        }

        Vector2 targetPos = target.transform.position;
        Vector2 lookDir = targetPos - (Vector2)player.transform.position;
        RotateToDir(lookDir);
    }

    public void RotateToDir(Vector2 lookDir)
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        float rotZ = Mathf.Abs(angle);

        bool isLeft = (rotZ > 90);

        if (isLeft)
        {
            player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
