using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public static EnemyManager Instance { get; private set; }

    //public List<Enemy> activeEnemies;

<<<<<<< HEAD
    //// �� ������ (Inspector���� ����)
    //public GameObject enemyPrefab;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("���� ���� EnemyManager �ν��Ͻ��� �����մϴ�. �ϳ��� �����ؾ� �մϴ�.");
    //        Destroy(gameObject); // �ߺ� �ν��Ͻ� ó��
    //    }
    //}

    //public void SpawnEnemies(RoomManager room)
    //{
    //    // ���� ��ġ�� ���� ���� ����
    //    Vector2 spawnPosition = new Vector2(room.roomPos.x, room.roomPos.y); // ���÷� ���� �߽ɿ� ���� ����
    //    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    //    // Enemy ��ü�� �����ͼ� ����Ʈ�� �߰�
    //    Enemy enemy = enemyObject.GetComponent<Enemy>();
    //    activeEnemies.Add(enemy);

    //    // ���� ���� �� OnEnemyDefeated ȣ��
    //    enemy.OnDeath += () => OnEnemyDefeated(enemy);
    //}

    //public void OnEnemyDefeated(RoomManager room, Enemy enemy)
    //{
    //    // ����Ʈ���� �ش� �� ����
    //    activeEnemies.Remove(enemy);

    //    // ��� ���� óġ�Ǿ����� Ȯ��
    //    if (activeEnemies.Count == 0)
    //    {
    //        // ��� ���� óġ�Ǿ����Ƿ� �� Ŭ���� ó��
    //        RoomManager room = enemy.GetComponent<RoomManager>();  // ���� ���� ���� ã�Ƽ�
    //        room.OnAllEnemiesDefeated();  // ���� ��� ���� óġ������ �˸�
    //    }
    //}

    //public void ClearAllEnemies()
    //{
    //    foreach (var enemy in activeEnemies)
    //    {
    //        Destroy(enemy.gameObject);
    //    }
    //    activeEnemies.Clear();
=======
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

>>>>>>> parent of 142ca4f (Init : 프리펩 생성 및 애너미 매니저)
    //}
}
