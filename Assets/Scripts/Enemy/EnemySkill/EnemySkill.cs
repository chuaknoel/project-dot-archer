using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public string skillName;
    public float damage;
    public float cooldown=3F;
    public float currentCooldown;
    public GameObject effect;



    // Update is called once per frame
    public bool CanUse()
    {           
        return currentCooldown > cooldown;
    }
    
    public virtual void UseSkill(BaseEnemy owner)
    {
        currentCooldown = 0;
    }

}
