using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager : MonoBehaviour
{
    public List<Skill<Player>> skillList;

    public Transform equipSkillSlot;

    public Skill<Player> CreateSkill(int skillNum)
    {
        Skill<Player> newSkill = Instantiate(skillList[skillNum], equipSkillSlot);
        return newSkill;
    }
}
