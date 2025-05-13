using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private int maxRooms = 10;
    [SerializeField] private int longRoomIndex = 5;
    private bool hasPlacedLongRoom = false;

    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private Dictionary<Vector2Int, Room> rooms = new();
    public Dictionary<Vector2Int, Room> Rooms => rooms;

    [SerializeField] private RoomFactory roomFactory;
    [SerializeField] private DoorSpawner doorSpawner;

    public Dictionary<Vector2Int, Room> GenerateDungeon()
    {
        rooms.Clear();
        Vector2Int currentPos = Vector2Int.zero;
        Room startRoom = roomFactory.CreateRoom(currentPos, ROOMTYPE.Normal);
        rooms.Add(currentPos, startRoom);

        Queue<Vector2Int> frontier = new();
        frontier.Enqueue(currentPos);

        int roomCount = 1;

        while (roomCount < maxRooms && frontier.Count > 0)
        {
            Vector2Int parentPos = frontier.Dequeue();
            ShuffleDirections();

            foreach (var dir in directions)
            {
                if (roomCount >= maxRooms) break;

                Vector2Int newPos = parentPos + dir;
                if (rooms.ContainsKey(newPos)) continue;

                // === 긴방 배치 조건 ===
                if (!hasPlacedLongRoom && roomCount == longRoomIndex)
                {
                    Vector2Int left = newPos + Vector2Int.left;
                    Vector2Int right = newPos + Vector2Int.right;

                    bool leftEmpty = !rooms.ContainsKey(left);
                    bool rightEmpty = !rooms.ContainsKey(right);

                    if ((leftEmpty && rightEmpty) || leftEmpty)
                    {
                        // 왼쪽 기준 긴방 생성
                        Room longRoom = roomFactory.CreateRoom(left, ROOMTYPE.Long);
                        rooms.Add(left, longRoom);
                        rooms.Add(newPos, longRoom);

                        // 입구는 newPos이므로, 다음 frontier는 left
                        frontier.Enqueue(left);
                        roomCount++;
                        hasPlacedLongRoom = true;
                        break;
                    }
                    else if (rightEmpty)
                    {
                        Room longRoom = roomFactory.CreateRoom(newPos, ROOMTYPE.Long);
                        rooms.Add(newPos, longRoom);
                        rooms.Add(right, longRoom);

                        // 입구는 newPos → 다음 frontier는 right
                        frontier.Enqueue(right);
                        roomCount++;
                        hasPlacedLongRoom = true;
                        break;
                    }
                }

                // === 일반방 또는 보스방 ===
                ROOMTYPE type = (roomCount == maxRooms - 1) ? ROOMTYPE.Boss : ROOMTYPE.Normal;
                Room room = roomFactory.CreateRoom(newPos, type);
                rooms.Add(newPos, room);
                frontier.Enqueue(newPos);
                roomCount++;
                break;
            }
        }

        doorSpawner.SpawnDoors(rooms);
        return rooms;
    }

    private void ShuffleDirections()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            int rnd = Random.Range(0, directions.Length);
            (directions[i], directions[rnd]) = (directions[rnd], directions[i]);
        }
    }
}