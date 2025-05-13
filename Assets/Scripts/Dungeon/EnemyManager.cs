using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public static EnemyManager Instance { get; private set; }

    //public List<Enemy> activeEnemies;

<<<<<<< HEAD
    //// Àû ÇÁ¸®ÆÕ (Inspector¿¡¼­ ¼³Á¤)
    //public GameObject enemyPrefab;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("¿©·¯ °³ÀÇ EnemyManager ÀÎ½ºÅÏ½º°¡ Á¸ÀçÇÕ´Ï´Ù. ÇÏ³ª¸¸ À¯ÁöÇØ¾ß ÇÕ´Ï´Ù.");
    //        Destroy(gameObject); // Áßº¹ ÀÎ½ºÅÏ½º Ã³¸®
    //    }
    //}

    //public void SpawnEnemies(RoomManager room)
    //{
    //    // ¹æÀÇ À§Ä¡¿¡ ¸ÂÃç ÀûÀ» »ı¼º
    //    Vector2 spawnPosition = new Vector2(room.roomPos.x, room.roomPos.y); // ¿¹½Ã·Î ¹æÀÇ Áß½É¿¡ ÀûÀ» »ı¼º
    //    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    //    // Enemy °´Ã¼¸¦ °¡Á®¿Í¼­ ¸®½ºÆ®¿¡ Ãß°¡
    //    Enemy enemy = enemyObject.GetComponent<Enemy>();
    //    activeEnemies.Add(enemy);

    //    // ÀûÀÌ Á×À» ¶§ OnEnemyDefeated È£Ãâ
    //    enemy.OnDeath += () => OnEnemyDefeated(enemy);
    //}

    //public void OnEnemyDefeated(RoomManager room, Enemy enemy)
    //{
    //    // ¸®½ºÆ®¿¡¼­ ÇØ´ç Àû Á¦°Å
    //    activeEnemies.Remove(enemy);

    //    // ¸ğµç ÀûÀÌ Ã³Ä¡µÇ¾ú´ÂÁö È®ÀÎ
    //    if (activeEnemies.Count == 0)
    //    {
    //        // ¸ğµç ÀûÀÌ Ã³Ä¡µÇ¾úÀ¸¹Ç·Î ¹æ Å¬¸®¾î Ã³¸®
    //        RoomManager room = enemy.GetComponent<RoomManager>();  // ÀûÀÌ ¼ÓÇÑ ¹æÀ» Ã£¾Æ¼­
    //        room.OnAllEnemiesDefeated();  // ¹æÀÇ ¸ğµç ÀûÀ» Ã³Ä¡ÇßÀ½À» ¾Ë¸²
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
    //    // ¹æ À§Ä¡ ±â¹İÀ¸·Î Àû »ı¼º
    //    Enemy e = Instantiate(...);
    //    e.OnDeath += () => OnEnemyDefeated(room, e);
    //    RegisterEnemy(e);
    //}

    //public void OnEnemyDefeated(RoomManager room, Enemy enemy)
    //{
    //    UnregisterEnemy(enemy);

    //    // ÇØ´ç ¹æ¿¡ ³²Àº ÀûÀÌ ¾øÀ¸¸é
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

>>>>>>> parent of 142ca4f (Init : í”„ë¦¬í© ìƒì„± ë° ì• ë„ˆë¯¸ ë§¤ë‹ˆì €)
    //}
}
