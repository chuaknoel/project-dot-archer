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
            Debug.Log("�������� ������ �����ϴ�.");
            return;
        }
    }

    public List<BaseEnemy> SpawnEnemies(Room room)
    {
        Bounds roomSize = room.tilemap.GetComponent<TilemapRenderer>().bounds; //�ش� ���� floor bounds�� ���Ѵ�.

        var min = roomSize.min; //�ٿ�� min ����
        var max = roomSize.max; //�ٿ�� max ���� ���ؼ�

        int enemyCount = Random.Range(5, 9);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3(    //�� floor ������ ������ ��ġ ��ǥ�� �޾ƿ´�.
                  Random.Range(min.x+2, max.x-2),
                  Random.Range(min.y+2, max.y-2),
                  0
              );

            
            BaseEnemy e = Instantiate(
                currnetEnemyGroup[Random.Range(1, currnetEnemyGroup.Length)], //0���� ���� ���ʹ� ������ ������ �⺻ ���ʹ̸� �����ϰ� ȣ��
                spawnPosition,                                                  //�� ���ο� �����ϰ� ��ġ�Ͽ� ��ȯ
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

    public void OnEnemyDefeated(BaseEnemy enemy) //���ʹ̰� ������ ȣ��
    {
        UnregisterEnemy(enemy);

        // �ش� �濡 ���� ���� ������
        if (activeEnemies.Count == 0)
        {
            Debug.Log("����� óġ");
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
