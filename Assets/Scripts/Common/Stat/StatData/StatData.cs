using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class StatData
{
    public int level;
    public int exp;
    public float baseHealth;
    public AttackStatData attackStat;
    public DefenceStatData defenceStat;
    public MoveStatData moveStat;
    public int cost;

    public StatData(StatData statData)
    {
        level = statData.level;
        exp = statData.exp;
        baseHealth = statData.baseHealth;
        attackStat = statData.attackStat;
        defenceStat = statData.defenceStat;
        moveStat = statData.moveStat;
        cost = statData.cost;
    }
}
