using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Goblin : BaseEnemy
{
    public List<EnemySkill> skills;
    public GoblinStat goblinStat;

    public override void Init()
    {
        base.Init();
        goblinStat = GetComponent<GoblinStat>();

    }
}
