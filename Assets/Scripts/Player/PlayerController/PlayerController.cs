using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : BaseController<Player>
{
    protected Player player;
    private Vector3 inputDir;

    private Action lookAction;

    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
        lookAction = LookEnemy;
        ChangeLook(false);      //현재 던전 생성 로직이 첫방은 Enemy가 없음. false처리해준다. 
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();

        if (CheckRotateState())     //대기와 이동중일 떄만 rotate 시키는 임시 방어 매서드
        {
            lookAction?.Invoke();
        }

        base.OnUpdate(deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeLook(false);
        }
    }

    public Vector3 GetInputDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        return inputDir;
    }

    public void ChangeLook(bool enemyAlive)
    {
        if (enemyAlive)
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

    // 스킬이나 다른 액션 스테이트에서 자신이 전이될 수 있는지는 확인하는 매서드
    // 스킬 사용이나 기타 행동들이 전이되면 안되는 상황에서 전이되는 것을 방지
    public bool IsActionAbleSate()
    {
        switch (currentState.GetType().Name)
        {
            case nameof(PlayerMoveState):
            case nameof(PlayerIdleState):
                return true;
            default:
                return false;
        }
    }

    //스킬 사용 중 애니메이션을 실행해야되는데 무기 방향이 고정되어 애니메이션이 제대로 작동하지 않아서 임시로 막아둔 매서드
    public bool CheckRotateState()
    {
        switch (currentState.GetType().Name)
        {
            case nameof(PlayerMoveState):
            case nameof(PlayerIdleState):
                return true;
            default:
                return false;
        }
    }
}
