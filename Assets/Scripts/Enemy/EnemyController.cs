using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private BaseEnemy ownerEnemy;
    private BaseStat ownerStat;
    private MoveController moveController;
    private RangeAttackController rangeAttackController;

    private void Update()
    {
        if (ownerStat is IMoveStat moveStat)
        {
            if (rangeAttackController.canMove)
                moveController.MoveToPlayer(ownerEnemy.target, moveStat);
        }

        if (ownerStat is IAttackRangeStat range && ownerStat is IAttackStat attack)
        {
            rangeAttackController.RangeAttack(attack.AttackDamage, range, ownerEnemy);
        }
    }

    public void Init(BaseEnemy ownerEnemy)
    {
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
        moveController = this.AddComponent<MoveController>();
        rangeAttackController = this.AddComponent<RangeAttackController>();
    }

}
