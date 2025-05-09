using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : BaseEnemy
{
    public List<EnemySkill> skills;
    public ZombieStat zombieStat;
    public override void Init()
    {
        base.Init();
        zombieStat = GetComponent<ZombieStat>();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDefenceStat>(out IDefenceStat target))
            {
                target.TakeDamage(zombieStat.AttackDamage);
            }
        }
    }
}