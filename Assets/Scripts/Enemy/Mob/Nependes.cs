using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nependes : BaseEnemy
{
    public NependesStat nependesStat;

    public override void Init()
    {
        base.Init();
        nependesStat = GetComponent<NependesStat>();

        

    }
}
