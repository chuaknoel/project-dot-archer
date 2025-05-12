using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    //public List<Enemy> activeEnemies;

    //public void SpawnEnemies(RoomManager room)
    //{
    //    // 방 위치 기반으로 적 생성
    //    Enemy e = Instantiate(...);
    //    e.OnDeath += () => OnEnemyDefeated(room, e);
    //    RegisterEnemy(e);
    //}

    //public void OnEnemyDefeated(RoomManager room, Enemy enemy)
    //{
    //    UnregisterEnemy(enemy);

    //    // 해당 방에 남은 적이 없으면
    //    if (GetEnemiesInRoom(room).Count == 0)
    //    {
    //        room.OnAllEnemiesDefeated();
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

    //public void RegisterEnemy(Enemy enemy)
    //{

    //}
    //public void UnregisterEnemy(Enemy enemy)
    //{

    //}
}
