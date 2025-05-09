using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour 
{
    public bool angryMode;
    public List<BossSkill> skills;
  
    private void Start()
    {
      
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
            Instantiate(skill.effect, transform.position, Quaternion.identity);
        }     
    }
}
