using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public bool angryMode;
    public List<BossSkill> skills = new List<BossSkill>();
  
    private void Start()
    {
        SettingSkills();
        UseSkill(SelectSkill());
    }
    public void SettingSkills()
    {
        GameObject effect01 = Resources.Load<GameObject>("Effects/BossEffect01");
        GameObject effect02 = Resources.Load<GameObject>("Effects/BossEffect02");
        GameObject effect03 = Resources.Load<GameObject>("Effects/BossEffect03");

        skills.Add(new BossSkill("BossSkill01", 10, 3, effect01));
        skills.Add(new BossSkill("BossSkill02", 10, 3, effect02));
        skills.Add(new BossSkill("BossSkill03", 10, 3, effect03));
    }
    //protected override void Damaged(int damage)
    //{

    //}
    //protected override void Reward()
    //{

    //}

    string SelectSkill()
    {
       string[] skillNames = { "BossSkill01", "BossSkill02", "BossSkill03" };
        int index = Random.Range(0, skillNames.Length);
        return skillNames[index];
    }
    void UseSkill(string skillName)
    { 
        // 이름에 맞는 스킬 사용
        var skill = skills.Find(s=>s.skillName == skillName);
        if(skill.CanUse())
        {
            skill.currentCooldown += Time.deltaTime;
            // 이펙트 발사
            skill.ExecuteEffect(transform,skill.effect);
        }     
    }
}
