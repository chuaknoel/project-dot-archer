using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : BaseEnemy
{
    public ZombieStat zombieStat;

    public override void Init()
    {
        base.Init();
        zombieStat = GetComponent<ZombieStat>();
        
        ThrowStone stoneskill = GetComponent<ThrowStone>();
        skills.Add(stoneskill);
        
    }
}