using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
