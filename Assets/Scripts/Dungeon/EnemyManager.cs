using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    //public List<Enemy> activeEnemies;

    //public void SpawnEnemies(RoomManager room)
    //{
    //    // �� ��ġ ������� �� ����
    //    Enemy e = Instantiate(...);
    //    e.OnDeath += () => OnEnemyDefeated(room, e);
    //    RegisterEnemy(e);
    //}

    //public void OnEnemyDefeated(RoomManager room, Enemy enemy)
    //{
    //    UnregisterEnemy(enemy);

    //    // �ش� �濡 ���� ���� ������
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
