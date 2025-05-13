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

    private Player player;

    private PlayerData playerData;

    public void Init(Player player, PlayerData playerData)
    {
        this.player = player;
        this.playerData = playerData;
        attackDamage = playerData.statData.attackStat.attackDamage;
        moveSpeed = playerData.statData.moveStat.moveSpeed;
        currentHealth = 100;
        maxHealth = 100;
    }

    public float GetTotalStatDamage()
    {
        // 인벤토리에서 계산된 총 데미지 사용
        return attackDamage + player.Inventory.GetTotalAttackBonus() + player.UpgradeManager.permanentUpgradeData.GetAttackDamage() + player.WeaponHandler.GetAttackDamage();
    }

    public float GetToTalDefence()
    {
        return defence + player.Inventory.GetTotalDefenseBonus() + player.UpgradeManager.permanentUpgradeData.GetDefence();
    }

    public float GetTotalMoveSpeed()
    {
        return moveSpeed + player.UpgradeManager.permanentUpgradeData.GetMoveSpeed();
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
