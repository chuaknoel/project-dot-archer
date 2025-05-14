using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllerManager : MonoBehaviour
{
    protected BaseEnemy ownerEnemy;
    protected BaseStat ownerStat;
    protected MoveController moveController;
    protected SkillController skillController;

    protected virtual void Update()
    {
        if (skillController.canMove && ownerStat is IMoveStat moveStat)
        {
            moveController.MoveToPlayer(ownerEnemy.target, moveStat);
        }
        if (skillController.canUse)
        {
            skillController.UseSkill(ownerEnemy);
        }
    }

    public virtual void Init(BaseEnemy ownerEnemy)
    {
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
        moveController = this.AddComponent<MoveController>();  
        InitSkillController();
        skillController.AddSkill(ownerEnemy);

    }
   
    public virtual void InitSkillController()
    {
        skillController = this.AddComponent<SkillController>();
    }
}
