using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                  Random.Range(min.x, max.x),
                  Random.Range(min.y, max.y),
                  0
              );

            
            BaseEnemy e = Instantiate(
                currnetEnemyGroup[Random.Range(1, currnetEnemyGroup.Length-1)], //0���� ���� ���ʹ� ������ ������ �⺻ ���ʹ̸� �����ϰ� ȣ��
                spawnPosition,                                                  //�� ���ο� �����ϰ� ��ġ�Ͽ� ��ȯ
                Quaternion.identity
                ).GetComponent<BaseEnemy>();

            RegisterEnemy(e);
        }

        return activeEnemies;
    }

    public void OnEnemyDefeated(BaseEnemy enemy) //���ʹ̰� ������ ȣ��
    {
        UnregisterEnemy(enemy);

        // �ش� �濡 ���� ���� ������
        if (activeEnemies.Count == 0)
        {
            DungeonManager.Instance.roomManager.OnAllEnemiesDefeated();
            DungeonManager.Instance.player.Controller.ChangeLook(false);
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
