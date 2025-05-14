using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectController : MonoBehaviour
{
    GameObject circleEffect;
    GameObject rangedAttackEffect01;
    GameObject rangedAttackEffect02;

    List<GameObject> rangedAttackEffectList= new List<GameObject>();
    public float effectPower = 5f;

    private void OnEnable()
    {
        circleEffect = Resources.Load<GameObject>("Effects/BossEffect");
        rangedAttackEffect01 = Resources.Load<GameObject>("Effects/BossRangedAttackEffect01");
        rangedAttackEffect02 = Resources.Load<GameObject>("Effects/BossRangedAttackEffect02");
        rangedAttackEffectList.Add(rangedAttackEffect01);
        rangedAttackEffectList.Add(rangedAttackEffect02);
    }

    // 이펙트 발사
    public void ExecuteEffect(Transform bossTransform, string skillName)
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
            // 장판 공격 발사
            case "BossSkill04":
                RangedAttack(bossTransform.position, 6f);
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
        effect01.GetComponent<Rigidbody2D>().AddForce(_dir * effectPower, ForceMode2D.Impulse);

        // ShootEffect(circleEffect, _dir, Quaternion.identity, 20);
    }
    // 3방향 각도 (중앙, 왼쪽, 오른쪽) 발사
    public void ThreeShot(Transform bossTransform, GameObject skillEffect)
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
            effect02.GetComponent<Rigidbody2D>().AddForce(rotatedDir * effectPower, ForceMode2D.Impulse);
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
            effect03.GetComponent<Rigidbody2D>().AddForce(effect03.transform.right * effectPower, ForceMode2D.Impulse);
        }
    }
    // 장판 공격
    public void RangedAttack(Vector2 position, float delay)
    {   
        GameObject _rangedAttackEffect = Instantiate(SelectRandomSkill(), position, Quaternion.identity);

        BossRangedAttackEffect bossRangedAttackEffect = _rangedAttackEffect.GetComponent<BossRangedAttackEffect>();
        bossRangedAttackEffect.StartCoroutine(bossRangedAttackEffect.ExecuteAttack(6f));
    }
    // 이펙트 랜덤 선택
    public GameObject SelectRandomSkill()
    {  
        int index = UnityEngine.Random.Range(0, 2);

        return rangedAttackEffectList[index];
    }
}
