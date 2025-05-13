using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : EnemySkill
{
    public BossSkill(string name, int damage, float cooldown, GameObject effect)
    {
        this.skillName = name;
        this.damage = damage;
        this.cooldown = cooldown;
    }
    
}