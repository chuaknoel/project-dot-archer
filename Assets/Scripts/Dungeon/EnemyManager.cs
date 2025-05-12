using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public static EnemyManager Instance { get; private set; }

    //public List<Enemy> activeEnemies;

    //// 적 프리팹 (Inspector에서 설정)
    //public GameObject enemyPrefab;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("여러 개의 EnemyManager 인스턴스가 존재합니다. 하나만 유지해야 합니다.");
    //        Destroy(gameObject); // 중복 인스턴스 처리
    //    }
    //}

    //public void SpawnEnemies(RoomManager room)
    //{
    //    // 방의 위치에 맞춰 적을 생성
    //    Vector2 spawnPosition = new Vector2(room.roomPos.x, room.roomPos.y); // 예시로 방의 중심에 적을 생성
    //    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    //    // Enemy 객체를 가져와서 리스트에 추가
    //    Enemy enemy = enemyObject.GetComponent<Enemy>();
    //    activeEnemies.Add(enemy);

    //    // 적이 죽을 때 OnEnemyDefeated 호출
    //    enemy.OnDeath += () => OnEnemyDefeated(enemy);
    //}

    //public void OnEnemyDefeated(RoomManager room, Enemy enemy)
    //{
    //    // 리스트에서 해당 적 제거
    //    activeEnemies.Remove(enemy);

    //    // 모든 적이 처치되었는지 확인
    //    if (activeEnemies.Count == 0)
    //    {
    //        // 모든 적이 처치되었으므로 방 클리어 처리
    //        RoomManager room = enemy.GetComponent<RoomManager>();  // 적이 속한 방을 찾아서
    //        room.OnAllEnemiesDefeated();  // 방의 모든 적을 처치했음을 알림
    //    }
    //}

    //public void ClearAllEnemies()
    //{
    //    foreach (var enemy in activeEnemies)
    //    {
    //        Destroy(enemy.gameObject);
    //    }
    //    activeEnemies.Clear();
    //}
}
