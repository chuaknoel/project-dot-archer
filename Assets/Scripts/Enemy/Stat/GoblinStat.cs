using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStat : EnemyStat ,  IAttackStat, IMoveStat
{
    public float AttackDamage => attackDamage;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private float attackDamage;
    [SerializeField] private float moveSpeed;
    
   

}
