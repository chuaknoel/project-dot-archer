using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveStat
{
    float MoveSpeed { get; }

    float BuffSpeed { get; }

    public void AddSpeed(float addSpeed)
    {

    }

    public float GetTotalSpeed()
    {
        return 0;
    }
}
