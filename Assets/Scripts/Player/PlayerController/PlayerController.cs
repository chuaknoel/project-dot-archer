using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : BaseController<Player>
{
    protected Player player;
    private Vector3 inputDir;

    public PlayerController(State<Player> initState, Player player) : base(initState, player)
    {
        this.player = player;
    }

    public override void OnUpdate(float deltaTime)
    {
        GetInputDir();
        LookRotate();
        base.OnUpdate(deltaTime);
    }

    public Vector3 GetInputDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        return inputDir;
    }

    public void LookRotate()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = worldPos - (Vector2)player.transform.position;
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

        player.WeaponHandler?.Rotate(rotZ);
    }
}
