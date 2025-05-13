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
    public string selectSkill; // ������ ��ų

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
    // ��ų ���� ����
    public void SettingSkills()
    {
        bossEffectController = GetComponent<BossEffectController>();

        // ��ų ���� ����
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
    // ��ų ����
    protected override IEnumerator RunSkill(BaseEnemy owner)
    {
        canUse = false;
        canMove = false;
        yield return new WaitForSeconds(0.5f);

         var skill =  bossSkills.Find(s => s.skillName.Equals(selectSkill));
        // ��ų�� ��� �����ϴٸ�
        if (skill.CanUse())
        {
            // ��ٿ� �ʱ�ȭ
            skill.CooldownInit();

            // ����Ʈ �߻�
            bossEffectController.ExecuteEffect(transform, skill.skillName);
        }
        yield return new WaitForSeconds(3f);


        canMove = true;
        canUse = true;

    }
    // ��ų ���� ����
    public BossSkill SelectRandomSkill()
    {
        string[] skillNames = { "BossSkill01", "BossSkill02", "BossSkill03", "BossSkill04" };
        int index = UnityEngine.Random.Range(0, skillCount);

        return bossSkills.Find(s => s.skillName.Equals(skillNames[index])); 
    }


       

}
