using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : EnemySkill
{
    public void Initialize(string name, int damage, float cooldown)
    {
        this.skillName = name;
        this.damage = damage;
        this.cooldown = cooldown;
    }
    // Update is called once per frame
    public override bool CanUse()
    {
         return currentCooldown <= 0f;
    }
    public void CooldownInit()
    {
        currentCooldown = cooldown;
    }
    public void UpdateCooldown()
    {
        if (currentCooldown > 0f)
            currentCooldown -= Time.deltaTime;
    }
}