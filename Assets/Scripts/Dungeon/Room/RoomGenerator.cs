using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

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

    private List<Room> rooms = new();
    public List<Room> Rooms => rooms;

    [SerializeField] private RoomFactory roomFactory;
    [SerializeField] private DoorSpawner doorSpawner;

    public List<Room> GenerateDungeon()
    {
        rooms.Clear();
        Vector2Int currentPos = Vector2Int.zero;
        Room startRoom = roomFactory.CreateRoom(currentPos, ROOMTYPE.Normal);
        rooms.Add(startRoom);

        Queue<Room> frontier = new();
        frontier.Enqueue(startRoom);

        int roomCount = 1;

        while (roomCount < maxRooms && frontier.Count > 0)
        {
            Room parentRoom = frontier.Dequeue();
            Vector2 parentPos = parentRoom.position;
            ShuffleDirections();

            foreach (var dir in directions)
            {
                if (roomCount >= maxRooms) break;

                Vector2 newPos = parentPos + dir;
                if (IsOccupied(newPos)) continue;

                // === 긴방 배치 ===
                if (!hasPlacedLongRoom && roomCount == longRoomIndex)
                {
                    Vector2 left = newPos + Vector2Int.left;
                    Vector2 right = newPos + Vector2Int.right;

                    bool leftEmpty = !IsOccupied(left);
                    bool rightEmpty = !IsOccupied(right);

                    if ((leftEmpty && rightEmpty) || leftEmpty)
                    {
                        Room longRoom = roomFactory.CreateLongRoom(left, newPos, ROOMTYPE.Long);
                        rooms.Add(longRoom);
                        roomCount++;
                        hasPlacedLongRoom = true;
                        frontier.Enqueue(longRoom);
                        break;
                    }
                    else if (rightEmpty)
                    {
                        Room longRoom = roomFactory.CreateLongRoom(newPos, right, ROOMTYPE.Long);
                        rooms.Add(longRoom);
                        roomCount++;
                        hasPlacedLongRoom = true;
                        frontier.Enqueue(longRoom);
                        break;
                    }

                    // 양쪽 다 불가능한 경우 continue;
                    continue;
                }

                ROOMTYPE type = (roomCount == maxRooms - 1) ? ROOMTYPE.Boss : ROOMTYPE.Normal;

                // === 보스방 생성 ===
                if (type == ROOMTYPE.Boss)
                {
                    if (TryPlaceBossRoom(newPos, rooms.SelectMany(r => r.GetAllOccupiedPositions()).ToList(), out Vector2 center, out List<Vector2> bossOccupied))
                    {
                        Room bossRoom = roomFactory.CreateRoom(center, ROOMTYPE.Boss);
                        rooms.Add(bossRoom);
                        roomCount++;
                        break; // 보스방 한 번만 생성
                    }
                    else
                    {
                        continue; // 다른 방향으로 시도
                    }
                }
                else
                {
                    Room room = roomFactory.CreateRoom(newPos, type);
                    rooms.Add(room);
                    frontier.Enqueue(room);
                    roomCount++;
                }
                break; // 한 방향에만 생성
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

    private bool IsOccupied(Vector2 pos)
    {
        foreach (var room in rooms)
        {
            if (room.IsOccupyingPosition(pos)) return true;
        }
        return false;
    }

    private bool TryPlaceBossRoom(Vector2 basePos, List<Vector2> occupiedPositions, out Vector2 centerPos, out List<Vector2> occupied)
    {
        Vector2[][] patterns = new Vector2[][]
        {
            new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(0, 1) },
            new Vector2[] { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1) },
            new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) },
            new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1) },
        };

        foreach (var pattern in patterns)
        {
            List<Vector2> candidates = pattern.Select(offset => basePos + offset).ToList();
            if (candidates.All(pos => !occupiedPositions.Contains(pos)))
            {
                occupied = candidates;
                float avgX = candidates.Average(pos => pos.x);
                float avgY = candidates.Average(pos => pos.y);
                centerPos = new Vector2(avgX, avgY);
                return true;
            }
        }

        centerPos = Vector2.zero;
        occupied = null;
        return false;
    }
}