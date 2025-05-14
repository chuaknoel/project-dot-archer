using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkillExecutor
{
    private const int skillSlot = 5;
    private Skill<Player>[] skillList = new Skill<Player>[skillSlot];
    private Player player;

    public SkillExecutor(Player player, List<Skill<Player>> skillList)
    {
        this.player = player;

        for (int i = 0; i < skillList.Count; i++)
        {
            if (i < skillSlot)
            {
                this.skillList[i] = DungeonManager.Instance.skillManager.CreateSkill(i);
                this.skillList[i].Init(player);
            }
            else
            {
                Debug.Log("스킬을 더 이상 장작할 수 없습니다.");
            }
        }
    }

    public void OnUpdate(float deltaTime)
    {
        TryUseSkill();
    }

    public void TryUseSkill()
    {
        var (skillNum, canUse) = CheckInputKey();

        if (!canUse) return;

        if (player.Controller.IsActionAbleSate() && skillList[skillNum].CanUse())
        {
            player.Controller.ChangeState(nameof(PlayerSkillState));
            UseSkill(skillNum);
        }
    }

    private (int, bool) CheckInputKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return (0, true);
        if (Input.GetKeyDown(KeyCode.Alpha2)) return (1, true);
        if (Input.GetKeyDown(KeyCode.Alpha3)) return (2, true);
        if (Input.GetKeyDown(KeyCode.Alpha4)) return (3, true);
        if (Input.GetKeyDown(KeyCode.Alpha5)) return (4, true);

        return (-1, false);
    }

    public bool RegisterSkill(Skill<Player> skill)
    {
        if (skillList.Contains(skill))
        {
            Debug.Log("이미 등록된 스킬입니다.");
            return false;
        }

        for (int i = 0; i < skillSlot; i++)
        {
            if(skillList[i] == null)
            {
                skillList[i] = skill;
                skill.Init(player);
                return true;
            }
        }

        Debug.Log("빈 스킬 슬롯이 없습니다.");
        return false;
       
    }

    public void UnregisterSkill(int slotNum) 
    {
        Debug.Log("스킬 슬롯 비우기");
    }

    public void UseSkill(int skillNum)
    {
        skillList[skillNum].UseSkill();
    }
}
