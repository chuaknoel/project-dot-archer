using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat, IAttackStat, IDefenceStat, IMoveStat
{
    [SerializeField] private float attackDamage;
    public float AttackDamage => attackDamage;

    [SerializeField] private float defence;
    public float Defence => defence;

    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    private float buffDamage;
    public float BuffDamage => buffDamage;

    private float buffSpeed;
    public float BuffSpeed => buffSpeed;

    private Player player;

    public PlayerStat(Player player, PlayerData playerData)
    {
        this.player = player;
        attackDamage = playerData.statData.attackStat.attackDamage;
        moveSpeed = playerData.statData.moveStat.moveSpeed;
        currentHealth = 100;
        maxHealth = 100;
    }

    public void Init(Player player)
    {
        this.player = player;
    }

    public float TotalDamage()
    {
        // 인벤토리에서 계산된 총 데미지 사용
        return attackDamage + player.Inventory.GetTotalAttackBonus();
    }

    public void TakeDamage(float damage)
    {
        if (IsDeath) return;
        float applyDamage = Mathf.Clamp(damage - Defence, 0, damage - Defence);
        currentHealth -= applyDamage;

        if (IsDeath)
        {
            Death();
        }
    }

    public override void Death()
    {
        base.Death();
        player.Controller.ChangeState(nameof(PlayerDeathState));
    }
}
