using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salamandra : BaseEnemy
{
    public SalamandraStat salamandraStat;

    public override void Init()
    {
        base.Init();
        salamandraStat = GetComponent<SalamandraStat>();



    }
}
