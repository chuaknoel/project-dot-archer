using System.Collections.Generic;
using UnityEngine;
using Enums;


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


    public void SpawnDoors(List<Room> rooms)
    {
        HashSet<(Vector2, Vector2)> processedPairs = new();
        HashSet<Room> processedRooms = new();

        foreach (var room in rooms)
        {
            if (processedRooms.Contains(room)) continue;
            processedRooms.Add(room);

            foreach (Vector2 roomPos in room.occupiedPositions)
            {
                foreach (var dir in directions)
                {
                    Vector2 neighborPos = roomPos + dir;

                    Room neighborRoom = FindRoomAtPosition(rooms, neighborPos);
                    if (neighborRoom == null || room == neighborRoom) continue;

                    // ½Ö Áßº¹ ¹æÁö
                    Vector2 a = Vector2.Min(roomPos, neighborPos);
                    Vector2 b = Vector2.Max(roomPos, neighborPos);
                    if (!processedPairs.Add((a, b))) continue;


                    Debug.Log($"Room : {room}, {room.occupiedPositions}, {-dir},{dir}");
                    Debug.Log($"{neighborRoom}, {room.occupiedPositions}, {dir}, {-dir}");
                    
                    CreateDoor(room, -dir, dir);
                    CreateDoor(neighborRoom,  dir, -dir);
                }
            }
        }
    }

    private Room FindRoomAtPosition(List<Room> rooms, Vector2 pos)
    {
        return rooms.Find(r => r.IsOccupyingPosition(pos));
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