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
    private BossSkill executionSkills; // ������ ��ų

    private void OnEnable()
    {
       
        SettingSkills();
     //   UseSkill();
    }
    public void SettingSkills()
    {
        GameObject effect = Resources.Load<GameObject>("Effects/BossEffect");
        Debug.Log("��ų���� "+ skillCount);

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
            Debug.Log("���� ��ų: " + s.skillName);
        }
        if (skill == null)
        {
            Debug.Log(index);
            Debug.LogError("��ų�� ã�� �� �����ϴ�: " + skillNames[index]+skill.skillName);
            return;
        }
        else
        {
            Debug.Log(index);
            Debug.LogError("ã�� ��ų : " + skillNames[index] + skill.skillName);

        }
        if (skill.CanUse())
        {
            // ��ų ��Ÿ��
            skill.currentCooldown += Time.deltaTime;
            // ����Ʈ �߻�
            ExecuteEffect(transform, skill.effect, skill.skillName);
        }
    }
    public void ExecuteEffect(Transform bossTransform, GameObject skillEffect,string skillName)
    {
        Debug.Log(skillEffect);
        switch (skillName)
        {
            // �ϳ��� �߻�
            case "BossSkill01":
                OneShot(bossTransform);
                break;
            // 3���� ���� (�߾�, ����, ������) �߻�
            case "BossSkill02":
                ThreeShot(bossTransform, skillEffect);
                break;
            // �� ��� �߻�
            case "BossSkill03":
                CircleShot(bossTransform);
                break;
        }
    }
    // �ϳ��� �߻�
    private void OneShot(Transform bossTransform)
    {
        GameObject effect01 = GameObject.Instantiate(executionSkills.effect, bossTransform.position, Quaternion.identity);
        Vector2 _dir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;
        effect01.GetComponent<Rigidbody2D>().AddForce(_dir * 10f, ForceMode2D.Impulse);
    }
    // 3���� ���� (�߾�, ����, ������) �߻�
    public static void ThreeShot(Transform bossTransform, GameObject effect)
    {
        float[] angles = { -15f, 0f, 15f };
        foreach (float angle in angles)
        {
            Vector2 centerDir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;

            // �߽� ���� �������� ���� ȸ��
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angle) * centerDir;

            GameObject effect02 = GameObject.Instantiate(effect, bossTransform.position, Quaternion.identity);
            effect02.GetComponent<Rigidbody2D>().AddForce(rotatedDir * 10f, ForceMode2D.Impulse);
        }
    }
    // �� ��� �߻�
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
