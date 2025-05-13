using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour, ISkillController
{
    public bool canMove = true;
    public bool canUse = true;

    public List<EnemySkill> skills = new List<EnemySkill>();

    
    public void AddSkill(BaseEnemy owner)
    {
        skills.AddRange(owner.GetComponents<EnemySkill>());
    }


    public virtual void UseSkill(BaseEnemy owner)
    {
        if (!canUse || skills.Count == 0 || skills == null) return;
        StartCoroutine(RunSkill(owner));

    }

    private IEnumerator RunSkill(BaseEnemy owner)
    {
        canUse = false;

        yield return new WaitForSeconds(1.5f);

        canMove = false;

        foreach (var skill in skills)
        {
            if (skill.CanUse())
            {
                skill.UseSkill(owner);  
            }
        }
        yield return new WaitForSeconds(1.5f);


        canMove = true;
        canUse = true;

    }


}
