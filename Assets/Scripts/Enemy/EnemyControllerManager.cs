using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllerManager : MonoBehaviour
{
    private BaseEnemy ownerEnemy;
    private BaseStat ownerStat;
    private MoveController moveController;
    private SkillController skillController;

    private void Update()
    {
        if (skillController.canMove && ownerStat is IMoveStat moveStat )
        {
            moveController.MoveToPlayer(ownerEnemy.target, moveStat);
        }

        if (skillController.canUse)
        {
            Debug.Log("Can Use Skill");
            skillController.UseSkill(ownerEnemy);
        }


    }

    public void Init(BaseEnemy ownerEnemy)
    {
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
        moveController = this.AddComponent<MoveController>();
        skillController = this.AddComponent<SkillController>();
        skillController.AddSkill(ownerEnemy);

    }

}
