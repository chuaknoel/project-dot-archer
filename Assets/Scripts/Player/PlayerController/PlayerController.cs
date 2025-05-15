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
        ChangeLook(false);      //���� ���� ���� ������ ù���� Enemy�� ����. falseó�����ش�. 
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();

        if (CheckRotateState())     //���� �̵����� ���� rotate ��Ű�� �ӽ� ��� �ż���
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

    // ��ų�̳� �ٸ� �׼� ������Ʈ���� �ڽ��� ���̵� �� �ִ����� Ȯ���ϴ� �ż���
    // ��ų ����̳� ��Ÿ �ൿ���� ���̵Ǹ� �ȵǴ� ��Ȳ���� ���̵Ǵ� ���� ����
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

    //��ų ��� �� �ִϸ��̼��� �����ؾߵǴµ� ���� ������ �����Ǿ� �ִϸ��̼��� ����� �۵����� �ʾƼ� �ӽ÷� ���Ƶ� �ż���
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
