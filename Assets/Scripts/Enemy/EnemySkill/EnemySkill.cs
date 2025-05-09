using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    public string skillName;
    private float damage;
    private float cooldown=3F;
    public float currentCooldown;
    public GameObject effect;



    // Update is called once per frame
    public bool CanUse()
    {           
        return currentCooldown < cooldown;
    }
    
    public virtual void UseSkill()
    {

    }

}
