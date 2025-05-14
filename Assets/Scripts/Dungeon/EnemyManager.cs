using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<BaseEnemy> activeEnemies;

    public GameObject[] enemyPrefabs;

    public BaseEnemy[] stage_one_enemies;
    public BaseEnemy[] stage_two_enemies;

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
