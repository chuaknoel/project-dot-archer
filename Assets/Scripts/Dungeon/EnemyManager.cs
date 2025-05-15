using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    public List<BaseEnemy> activeEnemies;

    public GameObject[] enemyPrefabs;

    public BaseEnemy[] stage_one_enemies;
    public BaseEnemy[] stage_two_enemies;

    public BaseEnemy[] currnetEnemyGroup;

    public void Init()
    {
        if (DungeonManager.dungeonStage == 1)
        {
            currnetEnemyGroup = stage_one_enemies;
        }
        else if (DungeonManager.dungeonStage == 2)
        {
            currnetEnemyGroup = stage_two_enemies;
        }
        else
        {
            Debug.Log("스테이지 정보가 없습니다.");
            return;
        }
    }

    public List<BaseEnemy> SpawnEnemies(Room room)
    {
        Bounds roomSize = room.tilemap.GetComponent<TilemapRenderer>().bounds; //해당 룸의 floor bounds를 구한다.

        var min = roomSize.min; //바운스의 min 값과
        var max = roomSize.max; //바운스의 max 값을 구해서

        int enemyCount = Random.Range(5, 9);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3(    //룸 floor 내부의 랜덤한 위치 좌표를 받아온다.
                  Random.Range(min.x+2, max.x-2),
                  Random.Range(min.y+2, max.y-2),
                  0
              );

            
            BaseEnemy e = Instantiate(
                currnetEnemyGroup[Random.Range(1, currnetEnemyGroup.Length)], //0번은 보스 에너미 임으로 제외한 기본 에너미를 랜던하게 호출
                spawnPosition,                                                  //룸 내부에 랜덤하게 위치하여 소환
                Quaternion.identity
                ).GetComponent<BaseEnemy>();

            RegisterEnemy(e);
        }

        return activeEnemies;
    }

    public List<BaseEnemy> SpawnBoss(Room bossRoom)
    {
        Vector3 spawnPosition = bossRoom.transform.position;

        BaseEnemy e = Instantiate(
            currnetEnemyGroup[0],
            spawnPosition,
            Quaternion.identity
            ).GetComponent<BaseEnemy>();

        RegisterEnemy(e);

        return activeEnemies;
    }

    public void OnEnemyDefeated(BaseEnemy enemy) //에너미가 죽으면 호출
    {
        UnregisterEnemy(enemy);

        // 해당 방에 남은 적이 없으면
        if (activeEnemies.Count == 0)
        {
            Debug.Log("모든적 처치");
            DungeonManager.Instance.player.Controller.ChangeLook(false);
            DungeonManager.Instance.roomManager.OnAllEnemiesDefeated();
        }
    }

    public void OnBossDefeated(BaseEnemy boss)
    {
        UnregisterEnemy(boss);
        DungeonManager.Instance.roomManager.OnNextStage();
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
