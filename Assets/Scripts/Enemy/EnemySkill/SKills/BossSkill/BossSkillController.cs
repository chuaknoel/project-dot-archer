using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillController : SkillController, ISkillController
{
    public bool angryMode;
    public int skillCount;
    public List<GameObject> effects = new List<GameObject>();
    private  List<BossSkill> bossSkills = new List<BossSkill>();
    private BossSkill executionSkills; // 실행할 스킬

    private void OnEnable()
    {
       
        SettingSkills();
     //   UseSkill();
    }
    public void SettingSkills()
    {
        GameObject effect = Resources.Load<GameObject>("Effects/BossEffect");
        Debug.Log("스킬세팅 "+ skillCount);

        BossEnemy bossEnemy = GetComponent<BossEnemy>();
        if (bossEnemy.bossIndex == 1)
        {
            skillCount = 2;
        }
        else
        {
            skillCount = 3;
        }

        for (int i = 1; i <= skillCount; i++)
        {
            string skillName = $"BossSkill{i:00}";
            bossSkills.Add(new BossSkill(skillName, 10*i, 3*i, effect));
        }
    }
    //public string SelectSkill()
    //{
    //    string[] skillNames = { "BossSkill01", "BossSkill02", "BossSkill03" };
    //    int index = UnityEngine.Random.Range(0, skillNames.Length);
    //    return skillNames[index];
    //}
    public override void UseSkill(BaseEnemy owner)
    {
        string[] skillNames = { "BossSkill01", "BossSkill02", "BossSkill03" };
        int index = UnityEngine.Random.Range(0, skillCount);
     
        Debug.Log(skillNames[index]);
        // var skill = skills.Find(s => s.skillName == skillNames[index]);
        // var skill = bossSkills.Find(s => s.skillName.Trim() == skillNames[index].Trim());
        var skill = bossSkills.Find(s => s.skillName.Equals(skillNames[index]));
        Debug.Log(skill);
   

       
        foreach (var s in bossSkills)
        {
            Debug.Log("보유 스킬: " + s.skillName);
        }
        if (skill == null)
        {
            Debug.Log(index);
            Debug.LogError("스킬을 찾을 수 없습니다: " + skillNames[index]+skill.skillName);
            return;
        }
        else
        {
            Debug.Log(index);
            Debug.LogError("찾은 스킬 : " + skillNames[index] + skill.skillName);

        }
        if (skill.CanUse())
        {
            // 스킬 쿨타임
            skill.currentCooldown += Time.deltaTime;
            // 이펙트 발사
            ExecuteEffect(transform, skill.effect, skill.skillName);
        }
    }
    public void ExecuteEffect(Transform bossTransform, GameObject skillEffect,string skillName)
    {
        Debug.Log(skillEffect);
        switch (skillName)
        {
            // 하나만 발사
            case "BossSkill01":
                OneShot(bossTransform);
                break;
            // 3방향 각도 (중앙, 왼쪽, 오른쪽) 발사
            case "BossSkill02":
                ThreeShot(bossTransform, skillEffect);
                break;
            // 원 모양 발사
            case "BossSkill03":
                CircleShot(bossTransform);
                break;
        }
    }
    // 하나만 발사
    private void OneShot(Transform bossTransform)
    {
        GameObject effect01 = GameObject.Instantiate(executionSkills.effect, bossTransform.position, Quaternion.identity);
        Vector2 _dir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;
        effect01.GetComponent<Rigidbody2D>().AddForce(_dir * 10f, ForceMode2D.Impulse);
    }
    // 3방향 각도 (중앙, 왼쪽, 오른쪽) 발사
    public static void ThreeShot(Transform bossTransform, GameObject effect)
    {
        float[] angles = { -15f, 0f, 15f };
        foreach (float angle in angles)
        {
            Vector2 centerDir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;

            // 중심 방향 기준으로 각도 회전
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angle) * centerDir;

            GameObject effect02 = GameObject.Instantiate(effect, bossTransform.position, Quaternion.identity);
            effect02.GetComponent<Rigidbody2D>().AddForce(rotatedDir * 10f, ForceMode2D.Impulse);
        }
    }
    // 원 모양 발사
    private void CircleShot(Transform bossTransform)
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject effect03 = GameObject.Instantiate(executionSkills.effect, bossTransform.position, rot);
            effect03.GetComponent<Rigidbody2D>().AddForce(effect03.transform.right * 10f, ForceMode2D.Impulse);
        }
    }
}
