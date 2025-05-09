using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseEnemy
{
    public List<EnemySkill> skills;
    public GoblinStat goblinStat;

    public override void Init()
    {
        base.Init();
        goblinStat = GetComponent<GoblinStat>();

    }

    

    protected  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage(goblinStat.AttackDamage);
            }
        }
    }



}
