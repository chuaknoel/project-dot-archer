using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private int maxRooms = 10;
    [SerializeField] private int longRoomIndex = 5;

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
        int nextLongRoomIndex = longRoomIndex;

        while (roomCount < maxRooms && frontier.Count > 0)
        {
            Vector2Int parentPos = frontier.Dequeue();
            ShuffleDirections();

            foreach (var dir in directions)
            {
                if (roomCount >= maxRooms) break;

                Vector2Int newPos = parentPos + dir;
                if (rooms.ContainsKey(newPos)) continue;

                // 긴방 배치 조건
                if (roomCount == nextLongRoomIndex && CanPlaceLongRoom(newPos, dir))
                {
                    Room longRoom = roomFactory.CreateRoom(newPos, ROOMTYPE.Long);
                    rooms.Add(newPos, longRoom);
                    rooms.Add(newPos + dir, longRoom);
                    frontier.Enqueue(newPos + dir);
                    roomCount++;
                    nextLongRoomIndex += 3; // 다음 긴 방 인덱스
                    break;
                }
                else if (!rooms.ContainsKey(newPos))
                {
                    ROOMTYPE type = (roomCount == maxRooms - 1) ? ROOMTYPE.Boss : ROOMTYPE.Normal;
                    Room room = roomFactory.CreateRoom(newPos, type);
                    rooms.Add(newPos, room);
                    frontier.Enqueue(newPos);
                    roomCount++;
                    break;
                }
            }
        }

        doorSpawner.SpawnDoors(rooms);
        return rooms;
    }

    private bool CanPlaceLongRoom(Vector2Int pos, Vector2Int dir)
    {
        if (dir == Vector2Int.left || dir == Vector2Int.right)
        {
            Vector2Int nextPos = pos + dir;
            return !rooms.ContainsKey(pos) && !rooms.ContainsKey(nextPos);
        }
        return false;
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