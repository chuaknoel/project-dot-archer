using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill
{
    public string skillName;
    protected float damage;
    protected float cooldown=3F;
    public float currentCooldown;
    public GameObject effect;

    // Update is called once per frame
    public bool CanUse()
    {           
        return currentCooldown < cooldown;
    }

}
