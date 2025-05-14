using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class StatData
{
    public int level;
    public int exp;
    public float maxHp;
    public AttackStatData attackStat;
    public DefenceStatData defenceStat;
    public MoveStatData moveStat;
    public int cost;

    public StatData(StatData statData)
    {
        level = statData.level;
        exp = statData.exp;
        maxHp = statData.maxHp;
        attackStat = statData.attackStat;
        defenceStat = statData.defenceStat;
        moveStat = statData.moveStat;
        cost = statData.cost;
    }

    public StatData(AttackStatData attackStat, MoveStatData moveStat)
    {
        this.attackStat = attackStat;
        this.moveStat = moveStat;
        this.cost = 10;
    }
}
