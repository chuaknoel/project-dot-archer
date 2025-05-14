using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySkill : EnemySkill
{
    virtual public void Init()
    {
        base.Init();
    }
    public override void UseSkill(BaseEnemy owner)
    {
        Debug.Log("Empty skill used");
        currentCooldown = 0f;
    }
}
