using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public List<BaseEnemy> activeEnemies;

    public GameObject[] enemyPrefabs;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("���� ���� DungeonManager �ν��Ͻ��� �����մϴ�. �ϳ��� �����ؾ� �մϴ�.");
            Destroy(gameObject); // Ȥ�� �ߺ� ���� ó��
        }
    }

    public void SpawnEnemies(RoomManager room)
    {
         Vector3 spawnPosition = new Vector3(
                    Random.Range(-3, 3),
                    Random.Range(-3, 3),
                    0
                ); // ���� ��ġ�� �������� �� ����
        // �� ��ġ ������� �� ����
        BaseEnemy e = Instantiate(enemyPrefabs[Random.Range(0, 6)], spawnPosition, Quaternion.identity).GetComponent<BaseEnemy>() ;
        //e.OnDeath += () => OnEnemyDefeated(room, e);
        RegisterEnemy(e);
    }

    public void OnEnemyDefeated(RoomManager room, BaseEnemy enemy)
    {
        UnregisterEnemy(enemy);

        // �ش� �濡 ���� ���� ������
        if (activeEnemies.Count == 0)
        {
            room.OnAllEnemiesDefeated();
        }
    }

    public void ClearAllEnemies()
    {
        foreach (var enemy in activeEnemies)
        {
            Destroy(enemy.gameObject);
        }
        activeEnemies.Clear();
    }

    public void RegisterEnemy(BaseEnemy enemy)
    {
        activeEnemies.Add(enemy);
    }
    public void UnregisterEnemy(BaseEnemy enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
