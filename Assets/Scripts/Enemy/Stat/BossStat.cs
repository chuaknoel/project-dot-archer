using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossStat : EnemyStat, IAttackStat, IMoveStat
{
    public float MoveSpeed => moveSpeed;
    public float AttackDamage => attackDamage;
    public float GetTotalStatDamage => GetTotalStatDamage;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;

    BossHpBarController hpBarController;
    public override void TakeDamage(float damage)
    {
        if (IsDeath) return;
        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage);
        currentHealth -= applyDamage;

        // HP Bar ������Ʈ
        UpdateHp();
        // �ǰ� �ִϸ��̼� ���
        PlayAnimation();

        if (IsDeath)
        {
            Death();
        }

    }
    protected override void UpdateHp()
    {
        hpBarController = GetComponent<BossHpBarController>();
        hpBarController.UpdateHP(CurrentHealth, MaxHealth);
    }
    protected override void PlayAnimation()
    {
        animator.SetBool("isDamaged", true);
        Invoke("EndDamaged", 0.15f);

    }
    float IAttackStat.GetTotalStatDamage()
    {
        return attackDamage;
    }
    public override void Death()
    {

        if (IsDeath)
        {
            GameManager.Instance.inventory.AddGold(1);                                               // ���� ����
            DungeonManager.Instance.enemyManager.OnEnemyDefeated(this.GetComponent<BaseEnemy>());    // ���ʹ� ���� üũ
            Destroy(hpBarController.hpBar);
            Destroy(this.gameObject);
            SceneManager.LoadScene("EndingScene");
            RoomManager.Instance.OnBossDefeated();
        }
    }
}
