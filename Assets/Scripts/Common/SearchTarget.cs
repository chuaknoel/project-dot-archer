using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    public List<BaseEnemy> targetList = new List<BaseEnemy>();

    private float targetDistance;
    private float nearestDistance;
    private int targetNum;

    public void SetTarget(List<BaseEnemy> targetList)
    {
        this.targetList = targetList;
    }

    public GameObject SearchNearestTarget()
    {
        if (targetList.Count == 0) return null;

        targetDistance = float.MaxValue;
        nearestDistance = float.MaxValue;

        for (int i = 0; i < targetList.Count; i++)
        {
            targetDistance = (transform.position - targetList[i].transform.position).magnitude;

            if(targetDistance < nearestDistance)
            {
                nearestDistance = targetDistance;
                targetNum = i;
            }
        }

        return targetList[targetNum].gameObject;
    }
}
