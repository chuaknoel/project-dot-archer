using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    public List<IDefenceStat> targetList = new List<IDefenceStat>();

    private float targetDistance;
    private float nearestDistance;

    public void SetTarget(List<IDefenceStat> targetList)
    {
        this.targetList = targetList;
    }

    public IDefenceStat SearchNearestTarget()
    {
        if (targetList.Count == 0) return null;

        targetDistance = 0;
        nearestDistance = 0;

        foreach (var target in targetList)
        {
            if(target is MonoBehaviour)
            {

            }
        }
        return targetList[0];
    }
}
