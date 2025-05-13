using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossSkillController : SkillController, ISkillController
{
    public bool angryMode;
    public int skillCount;
    public List<GameObject> effects = new List<GameObject>();
    private List<BossSkill> bossSkills = new List<BossSkill>();
    GameObject circleEffect;

    private void OnEnable()
    {
       
        SettingSkills();
     //   UseSkill();
    }
    private void Update()
    {
        foreach (var skill in bossSkills)
        {
            skill.UpdateCooldown();
        }
    }
    public void SettingSkills()
    {
        circleEffect = Resources.Load<GameObject>("Effects/BossEffect");

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
       //     bossSkills.Add(new BossSkill(skillName, 10*i, 3*i, effect));
       //     bossSkills.Add(bossSkill);

            var skill = gameObject.AddComponent<BossSkill>();
            skill.Initialize($"BossSkill{i:00}", 10 * i, 6 * i, circleEffect);
            bossSkills.Add(skill);
        }
        Debug.Log(bossSkills);
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
     
        //Debug.Log(skillNames[index]);
        //// var skill = skills.Find(s => s.skillName == skillNames[index]);
        //// var skill = bossSkills.Find(s => s.skillName.Trim() == skillNames[index].Trim());
        var skill = bossSkills.Find(s => s.skillName.Equals(skillNames[index]));


        if (skill.CanUse())
        {
            skill.CooldownInit(); // 쿨다운 초기화
            // 이펙트 발사
            ExecuteEffect(transform, skill.skillName);
        }
    }
    public void ExecuteEffect(Transform bossTransform,string skillName)
    {
        switch (skillName)
        {
            // 하나만 발사
            case "BossSkill01":
                OneShot(bossTransform);
                
                break;
            // 3방향 각도 (중앙, 왼쪽, 오른쪽) 발사
            case "BossSkill02":
                ThreeShot(bossTransform, circleEffect);
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

        GameObject effect01 = GameObject.Instantiate(circleEffect, bossTransform.position, Quaternion.identity);
        Vector2 _dir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;
        // 중력 제거
        effect01.GetComponent<Rigidbody2D>().gravityScale = 0f;
        effect01.GetComponent<Rigidbody2D>().AddForce(_dir * 20f, ForceMode2D.Impulse);

        // ShootEffect(circleEffect, _dir, Quaternion.identity, 20);
    }
    // 3방향 각도 (중앙, 왼쪽, 오른쪽) 발사
    public  void ThreeShot(Transform bossTransform, GameObject skillEffect)
    {
        float[] angles = { -15f, 0f, 15f };
        foreach (float angle in angles)
        {
            Vector2 centerDir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;

            // 중심 방향 기준으로 각도 회전
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angle) * centerDir;

            GameObject effect02 = GameObject.Instantiate(skillEffect, bossTransform.position, Quaternion.identity);
            // 중력 제거
            effect02.GetComponent<Rigidbody2D>().gravityScale = 0f;
            effect02.GetComponent<Rigidbody2D>().AddForce(rotatedDir * 20f, ForceMode2D.Impulse);
            //  ShootEffect(circleEffect, rotatedDir, Quaternion.identity, 20);
        }
    }
    // 원 모양 발사
    private void CircleShot(Transform bossTransform)
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject effect03 = GameObject.Instantiate(circleEffect, bossTransform.position, rot);
            // 중력 제거
            effect03.GetComponent<Rigidbody2D>().gravityScale = 0f;
            effect03.GetComponent<Rigidbody2D>().AddForce(effect03.transform.right * 20f, ForceMode2D.Impulse);

            //  ShootEffect(circleEffect, circleEffect.transform.right, rot,20);
        }
    }
    private void ShootEffect(GameObject effectPrefab, Vector2 direction, Quaternion quaternion, float speed)
    {
        Vector2 position = GameObject.FindGameObjectWithTag("Boss").transform.position;
            GameObject effect03 = GameObject.Instantiate(circleEffect, position, quaternion);
            // 중력 제거
            effect03.GetComponent<Rigidbody2D>().gravityScale = 0f;
            effect03.GetComponent<Rigidbody2D>().AddForce(direction* speed, ForceMode2D.Impulse);
        
    }
  }
