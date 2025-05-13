using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enemyAlive = !enemyAlive;
            ChangeLook();
        }
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
        var (angle, isLeft) = RotateToDir(lookDir);

        if (isLeft)
        {
            player.WeaponHandler?.Rotate(new Vector3(0, 180, 0));
        }
        else
        {
            player.WeaponHandler?.Rotate(new Vector3(0, 0, 0));
        }
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
        var (angle , isLeft) = RotateToDir(lookDir);
        player.WeaponHandler?.Rotate(new Vector3(0,0,angle));
    }

    public (float angle, bool isLeft) RotateToDir(Vector2 lookDir)
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        float rotZ = Mathf.Abs(angle);

        bool isLeft = (rotZ > 90);

        player.LookRotate(isLeft);
        return (angle , isLeft);
    }
}
