using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour // �� Ŭ���� ����, �� ���, ����ó��
{
    public Vector2Int roomPos;
    public bool isCleared;

    public void OnPlayerEnter()
    {
        if (!isCleared)
        {
            // EnemyManager�� ���� ���� ��û
            EnemyManager.Instance.SpawnEnemies(this);  // �� �濡 ���� �����ϵ��� ��û
        }
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // �� ���� �� ó��
    }
}
