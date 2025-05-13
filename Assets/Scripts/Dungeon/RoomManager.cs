using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour // 방 클리어 여부, 적 목록, 입장처리
{
    public Vector2Int roomPos;
    public bool isCleared;

    public void OnPlayerEnter()
    {
        if (!isCleared)
        {
            // EnemyManager에 몬스터 생성 요청
            EnemyManager.Instance.SpawnEnemies(this);  // 이 방에 적을 생성하도록 요청
        }
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // 문 열기 등 처리
    }
}
