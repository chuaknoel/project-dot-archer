using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgradeData : MonoBehaviour
{
    private GameData gameData;

    private float AttackDamage;
    private float Defence;
    private float MoveSpeed;

    private float Health;
    private float Cost;

    public void LoadData(GameData gameData)
    {
        AttackDamage = gameData.upgradeData.attackDamage;
        Defence = gameData.upgradeData.defence;
        MoveSpeed = gameData.upgradeData.moveSpeed;

        Health = gameData.upgradeData.health;
        Cost = gameData.upgradeData.cost;
    }

    public void UpgradeAttackDamage(float amount)
    {
        AttackDamage += amount;
        gameData.upgradeData.attackDamage += amount;
    }

    public void UpgradeDefence(float amount) 
    { 
        Defence += amount;
        gameData.upgradeData.defence += amount;
    }

    public void UpgradeMoveSpeed(float amout)
    {
        MoveSpeed += amout;
        gameData.upgradeData.moveSpeed += amout;
    }

    public void UpgradeHealth(float amout)
    {
        Health += amout;
        gameData.upgradeData.health += amout;
    }

    public void UpgradeCost(int amout)
    {
        Cost += amout;
        gameData.upgradeData.cost += amout;
    }

    public float GetAttackDamage()
    {
        return AttackDamage;
    }
    public float GetDefence()
    {
        return Defence;
    }
    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }
}
