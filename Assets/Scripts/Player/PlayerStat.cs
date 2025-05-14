using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerStat : BaseStat, IAttackStat, IDefenceStat, IMoveStat
{
    public float BaseHealth { get { return baseHealth; } }
    [SerializeField] protected float baseHealth;
    
    public float AttackDamage => attackDamage;
    [SerializeField] private float attackDamage;

    public float Defence => defence;
    [SerializeField] private float defence;

    public float MoveSpeed => moveSpeed;
    [SerializeField] private float moveSpeed;

    private int cost;
    private int useableCost;

    private float buffDamage;
    public float BuffDamage => buffDamage;

    private float buffSpeed;
    public float BuffSpeed => buffSpeed;

    private Player player;

    private GameData gameData;
    private PlayerData playerData;
    

    public void Init(Player player, GameData gameData)
    {
        this.gameData = gameData;
        this.player = player;
        this.playerData = gameData.playerData;
        attackDamage = playerData.statData.attackStat.attackDamage;
        defence = playerData.statData.defenceStat.defence;
        moveSpeed = playerData.statData.moveStat.moveSpeed;

        baseHealth = playerData.statData.baseHealth;
        maxHealth = baseHealth + gameData.upgradeData.health;
        currentHealth = maxHealth;
        cost = playerData.statData.cost + gameData.upgradeData.cost;
        useableCost = cost;
    }

    public float GetTotalStatDamage()
    {
        //플레이어 기본 공격력 + 장비 아이템 보너스 공격력 + 업그레이드로 올라간 공격력 
        return attackDamage + player.Inventory.GetTotalAttackBonus() + player.UpgradeManager.permanentUpgradeData.GetAttackDamage();
    }

    public float GetToTalDefence()
    {
        return defence + player.Inventory.GetTotalDefenseBonus() + player.UpgradeManager.permanentUpgradeData.GetDefence();
    }

    public float GetTotalMoveSpeed()
    {
        return moveSpeed + player.UpgradeManager.permanentUpgradeData.GetMoveSpeed();
    }

    public int GetUseableCost()
    {
        return useableCost;
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

    public void RecoverCost(int recoverCost)
    {
        useableCost = Mathf.Clamp((useableCost + recoverCost), (useableCost + recoverCost), cost);
    }

    public void RecoverHp(float recoverHp)
    {
        currentHealth = Mathf.Clamp((currentHealth + recoverHp), (currentHealth + recoverHp), maxHealth);
    }

    public override void Death()
    {
        base.Death();
        player.Controller.ChangeState(nameof(PlayerDeathState));
    }
}
