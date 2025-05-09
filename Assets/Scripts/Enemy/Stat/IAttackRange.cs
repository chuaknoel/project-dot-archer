using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public interface IAttackRangeStat
{
    float AttackRange { get; }
    float AttackDelay { get; }


    GameObject ProjectilePrefab { get; }
    float ProjectileSpeed { get; }

   
}
