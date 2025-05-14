using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;

    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public void SpawnDoors(Dictionary<Vector2Int, Room> rooms)
    {
        HashSet<(Vector2Int, Vector2Int)> processedPairs = new();

        HashSet<Room> processedRooms = new();

        foreach (var room in new HashSet<Room>(rooms.Values))
        {
            if (processedRooms.Contains(room)) continue;
            processedRooms.Add(room);

            foreach (var roomPos in room.occupiedPositions)
            {
                foreach (var dir in directions)
                {
                    Vector2Int neighborPos = roomPos + dir;
                    if (!rooms.ContainsKey(neighborPos)) continue;

                    Room neighborRoom = rooms[neighborPos];
                    if (room == neighborRoom) continue; // 같은 긴방 내부 연결 무시

                    // 쌍 중복 방지: 항상 (작은좌표, 큰좌표)로 정렬
                    Vector2Int a = Vector2Int.Min(roomPos, neighborPos);
                    Vector2Int b = Vector2Int.Max(roomPos, neighborPos);
                    if (!processedPairs.Add((a, b))) continue;

                    // 문 생성
                    CreateDoor(room, -dir, dir);
                    CreateDoor(neighborRoom, dir, -dir);
                }
            }
        }
    }

    private void CreateDoor(Room room, Vector2Int fromDir, Vector2Int toDir)
    {
        Vector3 spawnPos = room.GetEntryPositionFrom(fromDir);
        GameObject door = Instantiate(doorPrefab, spawnPos, Quaternion.identity, room.transform);

        if (toDir == Vector2Int.left)
            door.transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (toDir == Vector2Int.right)
            door.transform.rotation = Quaternion.Euler(0, 0, -90);
        else if (toDir == Vector2Int.down)
            door.transform.rotation = Quaternion.Euler(0, 0, 180);
        else
            door.transform.rotation = Quaternion.identity;

        door.GetComponent<EntryTrigger>().direction = toDir;
    }
}
