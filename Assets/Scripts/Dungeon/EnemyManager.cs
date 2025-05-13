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
            Debug.LogWarning("여러 개의 DungeonManager 인스턴스가 존재합니다. 하나만 유지해야 합니다.");
            Destroy(gameObject); // 혹은 중복 방지 처리
        }
    }

    public void SpawnEnemies(RoomManager room)
    {
         Vector3 spawnPosition = new Vector3(
                    Random.Range(-3, 3),
                    Random.Range(-3, 3),
                    0
                ); // 방의 위치를 기준으로 적 생성
        // 방 위치 기반으로 적 생성
        BaseEnemy e = Instantiate(enemyPrefabs[Random.Range(0, 6)], spawnPosition, Quaternion.identity).GetComponent<BaseEnemy>() ;
        //e.OnDeath += () => OnEnemyDefeated(room, e);
        RegisterEnemy(e);
    }

    public void OnEnemyDefeated(RoomManager room, BaseEnemy enemy)
    {
        UnregisterEnemy(enemy);

        // 해당 방에 남은 적이 없으면
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
