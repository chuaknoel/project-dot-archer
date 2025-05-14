using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData
{
    public float attackDamage;
    public float defence;
    public float moveSpeed;

    public float health;
    public int cost;

    public UpgradeData(UpgradeData upgradeData) 
    { 
        attackDamage = upgradeData.attackDamage;
        defence = upgradeData.defence;
        moveSpeed = upgradeData.moveSpeed;

        health = upgradeData.health;
        cost = upgradeData.cost;
    }
}
