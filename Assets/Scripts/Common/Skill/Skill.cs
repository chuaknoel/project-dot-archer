using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class Skill<T> : MonoBehaviour where T : MonoBehaviour
{
    public string skillName;
    public SkillType skillType;

    public float coolTime;
    public float amount;
    public float cost;

    public bool isUseable;

    public virtual void Init(T owner)
    {

    }

    public virtual void UseSkill()
    {

    }

    public virtual void CanUse()
    {

    }
}
