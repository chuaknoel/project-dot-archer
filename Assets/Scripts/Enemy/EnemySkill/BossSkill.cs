using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : EnemySkill
{
   public BossSkill(string name, int damage, float cooldown,GameObject effect)
    {
        this.skillName = name;
        this.damage = damage;
        this.cooldown = cooldown;
        this.effect = effect;
    }
    public void ExecuteEffect(Transform bossTransform, GameObject skillEffect)
    {
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
        GameObject effect01 = GameObject.Instantiate(effect, bossTransform.position, Quaternion.identity);
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
            GameObject effect03 = GameObject.Instantiate(effect, bossTransform.position, rot);
            effect03.GetComponent<Rigidbody2D>().AddForce(effect03.transform.right * 10f, ForceMode2D.Impulse);
        }
    }
}
