using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStat
{
    float AttackDamage { get; }

    float GetTotalStatDamage();
}
