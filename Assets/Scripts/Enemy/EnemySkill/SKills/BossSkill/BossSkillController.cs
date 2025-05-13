using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossSkillController : SkillController, ISkillController
{
  //  public bool angryMode;
    public int skillCount;
    BossEffectController bossEffectController;
    private List<BossSkill> bossSkills = new List<BossSkill>();
    public string selectSkill; // 선택한 스킬

    private void OnEnable()
    {      
        SettingSkills();
    }
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
            skill.Initialize($"BossSkill{i:00}", 10 * i, 6);
            bossSkills.Add(skill);
        }
    }

    public override void UseSkill(BaseEnemy owner)
    {
        if (!canUse || skills.Count == 0 || skills == null) return;

        StartCoroutine(RunSkill(owner));
    }
    // 스킬 실행
    protected override IEnumerator RunSkill(BaseEnemy owner)
    {
        canUse = false;
        canMove = false;
        yield return new WaitForSeconds(0.5f);

         var skill =  bossSkills.Find(s => s.skillName.Equals(selectSkill));
        // 스킬이 사용 가눙하다면
        if (skill.CanUse())
        {
            // 쿨다운 초기화
            skill.CooldownInit();

            // 이펙트 발사
            bossEffectController.ExecuteEffect(transform, skill.skillName);
        }
        yield return new WaitForSeconds(3f);


        canMove = true;
        canUse = true;

    }
    // 스킬 랜덤 선택
    public BossSkill SelectRandomSkill()
    {
        string[] skillNames = { "BossSkill01", "BossSkill02", "BossSkill03", "BossSkill04" };
        int index = UnityEngine.Random.Range(0, skillCount);

        return bossSkills.Find(s => s.skillName.Equals(skillNames[index])); 
    }


       

}
