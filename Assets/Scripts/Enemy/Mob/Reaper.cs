using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : BaseEnemy
{
    public ReaperStat reaperStat;

    public override void Init()
    {
        base.Init();
        reaperStat = GetComponent<ReaperStat>();

        ReaperSkill reaperSkill = GetComponent<ReaperSkill>();
        skills.Add(reaperSkill);
    }
}
