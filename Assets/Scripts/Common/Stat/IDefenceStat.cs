using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefenceStat
{
    float Defence { get; }

    void TakeDamage(float damage) { }
}
