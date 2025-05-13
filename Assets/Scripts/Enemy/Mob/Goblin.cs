using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseEnemy
{
    public GoblinStat goblinStat;

    public override void Init()
    {
        base.Init();
        goblinStat = GetComponent<GoblinStat>();

    }


}
