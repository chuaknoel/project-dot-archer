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

    // ����Ʈ �߻�
    public void ExecuteEffect(Transform bossTransform, string skillName)
    {
        switch (skillName)
        {
            // �ϳ��� �߻�
            case "BossSkill01":
                OneShot(bossTransform);
                break;
            // 3���� ���� (�߾�, ����, ������) �߻�
            case "BossSkill02":
                ThreeShot(bossTransform, circleEffect);
                break;
            // �� ��� �߻�
            case "BossSkill03":
                CircleShot(bossTransform);
                break;
            // ���� ���� �߻�
            case "BossSkill04":
                RangedAttack(bossTransform.position, 6f);
                break;
        }

    }
    // �ϳ��� �߻�
    private void OneShot(Transform bossTransform)
    {
        GameObject effect01 = GameObject.Instantiate(circleEffect, bossTransform.position, Quaternion.identity);
        Vector2 _dir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;
        // �߷� ����
        effect01.GetComponent<Rigidbody2D>().gravityScale = 0f;
        effect01.GetComponent<Rigidbody2D>().AddForce(_dir * effectPower, ForceMode2D.Impulse);

        // ShootEffect(circleEffect, _dir, Quaternion.identity, 20);
    }
    // 3���� ���� (�߾�, ����, ������) �߻�
    public void ThreeShot(Transform bossTransform, GameObject skillEffect)
    {
        float[] angles = { -15f, 0f, 15f };
        foreach (float angle in angles)
        {
            Vector2 centerDir = (GameObject.Find("Player").transform.position - bossTransform.position).normalized;

            // �߽� ���� �������� ���� ȸ��
            Vector2 rotatedDir = Quaternion.Euler(0, 0, angle) * centerDir;

            GameObject effect02 = GameObject.Instantiate(skillEffect, bossTransform.position, Quaternion.identity);
            // �߷� ����
            effect02.GetComponent<Rigidbody2D>().gravityScale = 0f;
            effect02.GetComponent<Rigidbody2D>().AddForce(rotatedDir * effectPower, ForceMode2D.Impulse);
        }
    }
    // �� ��� �߻�
    private void CircleShot(Transform bossTransform)
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            GameObject effect03 = GameObject.Instantiate(circleEffect, bossTransform.position, rot);
            // �߷� ����
            effect03.GetComponent<Rigidbody2D>().gravityScale = 0f;
            effect03.GetComponent<Rigidbody2D>().AddForce(effect03.transform.right * effectPower, ForceMode2D.Impulse);
        }
    }
    // ���� ����
    public void RangedAttack(Vector2 position, float delay)
    {   
        GameObject _rangedAttackEffect = Instantiate(SelectRandomSkill(), position, Quaternion.identity);

        BossRangedAttackEffect bossRangedAttackEffect = _rangedAttackEffect.GetComponent<BossRangedAttackEffect>();
        bossRangedAttackEffect.StartCoroutine(bossRangedAttackEffect.ExecuteAttack(6f));
    }
    // ����Ʈ ���� ����
    public GameObject SelectRandomSkill()
    {  
        int index = UnityEngine.Random.Range(0, 2);

        return rangedAttackEffectList[index];
    }
}
