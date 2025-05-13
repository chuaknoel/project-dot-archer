using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControllerManager : EnemyControllerManager
{   
    private ISkillController skillInterface;
     public float dis = 7f;

    public override void Init(BaseEnemy ownerEnemy)
    {
        base.Init(ownerEnemy);
        skillInterface = GetComponent<ISkillController>();
    }
    protected override void Update()
    {
        if (Distance())
        {
            if (skillController.canUse)
            {
                skillInterface.UseSkill(ownerEnemy);
            }
        }
        else
        {
            if (skillController.canMove && ownerStat is IMoveStat moveStat)
            {
                moveController.MoveToPlayer(ownerEnemy.target, moveStat);
            }
        }
    }

    bool Distance()
    {
        return Vector2.Distance(transform.position, DungeonManager.Instance.player.transform.position) <= dis ? true : false;
    }
    public override void InitSkillController()
    {
        skillController = this.AddComponent<BossSkillController>();
    }
}
