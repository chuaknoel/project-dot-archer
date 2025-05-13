using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill<Player>> skillList;

    private void Update()
    {
        
    }

    public void Init(Player player)
    {
        foreach (Skill<Player> skill in skillList)
        {
            skill.Init(player);
        }
    }

    public void UseSkill(int num)
    {
        if (skillList[num] != null)
        {
            skillList[num].UseSkill();
        }
    }
}
