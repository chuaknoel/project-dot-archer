using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.UIElements;

public class Skill<T> : MonoBehaviour where T : MonoBehaviour
{
    public string skillName;
    public SkillType skillType;

    public float coolTime;
    public float currentCoolTime;

    public int useCost;

    public float amount;

    public virtual void Init(T owner)
    {
        currentCoolTime = coolTime;
    }

    public virtual void UseSkill()
    {
        ApplyCooldown();
    }

    public virtual bool CanUse()
    {
        if (currentCoolTime < coolTime) { return false; }
        return true;
    }

    public void ApplyCooldown()
    {
        StartCoroutine(eApplayCooldown());
    }

    IEnumerator eApplayCooldown()
    {
        currentCoolTime = 0;
        yield return new WaitForSeconds(coolTime);
        currentCoolTime = coolTime;
    }
}
