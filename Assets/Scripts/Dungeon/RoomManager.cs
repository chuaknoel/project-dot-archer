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
            //EnemyManager.Instance.SpawnEnemies(this); // ��û�� ��
        }
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // �� ���� �� ó��
    }
}
