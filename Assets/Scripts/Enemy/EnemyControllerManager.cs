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
    private ISkillController skillInterface;
    public float dis = 3f;
    private void Update()
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

    public void Init(BaseEnemy ownerEnemy)
    {
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
        moveController = this.AddComponent<MoveController>();
        skillController = this.AddComponent<SkillController>();
        skillInterface = GetComponent<ISkillController>(); ;
        skillController.AddSkill(ownerEnemy);

    }
    bool Distance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return Vector2.Distance(transform.position, player.transform.position) <= dis ? true : false;
    }
}
