using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : BaseEnemy
{
    public List<EnemySkill> skills;
    public OrcStat orcStat;
    public override void Init()
    {
        base.Init();
        orcStat = GetComponent<OrcStat>();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage(orcStat.AttackDamage);
            }
        }
    }
}
