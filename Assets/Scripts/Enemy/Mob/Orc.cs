using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : BaseEnemy 
{
    public List<EnemySkill> skills;
    public OrcStat orcStat;
    public override void Init()
    {
        base.Init();
        orcStat = GetComponent<OrcStat>();
    }


    
}
