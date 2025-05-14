using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossSkillController : SkillController, ISkillController
{
  //  public bool angryMode;
    public int skillCount;
    BossEffectController bossEffectController;
    private List<BossSkill> bossSkills = new List<BossSkill>();
    public string selectSkill; // 선택한 스킬

    private void Update()
    {
        foreach (var skill in bossSkills)
        {
            skill.UpdateCooldown();
        }
    }
    // 스킬 정보 세팅
    public void SettingSkills()
    {
        bossEffectController = GetComponent<BossEffectController>();

        // 스킬 개수 설정
        skillCount = (GetComponent<BossEnemy>().bossIndex == 1) ? 2 : 4;

        for (int i = 1; i <= skillCount; i++)
        {
            var skill = gameObject.AddComponent<BossSkill>();
            skill.Initialize($"BossSkill{i:00}", 10 * i, 2);
            bossSkills.Add(skill);
        }
    }

    public override void UseSkill(BaseEnemy owner)
    {
        if (!canUse) return;
        StartCoroutine(RunSkill(owner));
    }
    // 스킬 실행
    protected override IEnumerator RunSkill(BaseEnemy owner)
    {
        canUse = false;
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        var skill = bossSkills.Find(s => string.Equals(s.skillName, selectSkill));

        // 스킬이 사용 가눙하다면
        if (skill.CanUse())
        {
            // 쿨다운 초기화
            skill.CooldownInit();

            // 이펙트 발사
            bossEffectController.ExecuteEffect(transform, skill.skillName);
        }

        yield return new WaitForSeconds(2.5f);

        canMove = true;
        canUse = true;

    }
    // 스킬 랜덤 선택
    public BossSkill SelectRandomSkill()
    {
        if (bossSkills == null || bossSkills.Count == 0)
        {
            SettingSkills();
        }

        int index = UnityEngine.Random.Range(0, bossSkills.Count);

        return bossSkills[index];
    }


       

}
