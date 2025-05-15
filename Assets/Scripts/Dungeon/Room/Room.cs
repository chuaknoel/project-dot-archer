using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

[System.Serializable]
public struct EntryInfo
{
    public Vector2 direction;
    public Vector3 position;

    public EntryInfo(Vector2 dir, Vector3 pos)
    {
        direction = dir;
        position = pos;
    }
}

public class Room : MonoBehaviour
{
    public Vector2 position; // 기준 좌표 (긴방은 왼쪽 기준)
    public List<Vector2> occupiedPositions = new();

    public Tilemap tilemap;
    [SerializeField] private TilemapRenderer roomBounds;

    public bool isVisited = false;
    public bool isCleared = false;
    public bool isBossRoom = false;

    public ROOMTYPE roomType;

    [SerializeField] private List<EntryInfo> entryPositions = new();

    public void Init(Vector2 pivot, ROOMTYPE type, Vector2 pos)
    {
        position = pos;
        roomType = type;
        occupiedPositions.Clear();

        if (tilemap == null)
        {
            tilemap = GetComponentInChildren<Tilemap>();
            if (tilemap == null)
            {
                Debug.LogError($"[Room] {name}: 하위에 Tilemap이 없습니다.");
                return;
            }
        }

        Bounds bounds = tilemap.localBounds;
        Vector3 roomWorldPosition;

        if (roomType == ROOMTYPE.Long)
        {
            occupiedPositions.Add(position);
            occupiedPositions.Add(position + Vector2Int.right);

            name = $"Room_Long_{position.x}_{position.y}_to_{position.x + 1}_{position.y}";

            Vector2 mid = (position + position + Vector2Int.right) / 2f;

            roomWorldPosition = new Vector3(
                mid.x * bounds.extents.x,
                mid.y * bounds.size.y,
                0f
            );
        }
        else if (roomType == ROOMTYPE.Boss)
        {
            occupiedPositions.Add(position);
            occupiedPositions.Add(position + Vector2Int.right);

            Vector2 mid1 = (position + position + Vector2Int.right) / 2f;
            Vector2 mid2 = (position + position + Vector2Int.right) / 2f;
            Vector2 mid = (mid1 + mid2) / 2f;

            name = $"Room_Boss_{position.x}_{position.y}";

            roomWorldPosition = new Vector3(
                mid.x * bounds.extents.x,
                mid.y * bounds.extents.y,
                0f
            );
        }
        else
        {
            occupiedPositions.Add(position);
            name = $"Room_{position.x}_{position.y}";

            roomWorldPosition = new Vector3(
                position.x * bounds.size.x,
                position.y * bounds.size.y,
                0f
            );
        }

        // 위치 보정
        transform.position = roomWorldPosition - bounds.center;

        // 입구 위치 설정
        entryPositions.Clear();
        Vector2 roomSize = new Vector2(18f, 10f);
        foreach (Vector2 occPos in occupiedPositions)
        {
            Vector3 worldCenter = GetWorldCenterOfTile(occPos, roomSize);

            entryPositions.Add(new EntryInfo(Vector2Int.left, worldCenter + new Vector3(-roomSize.x / 2f + 1.25f, 0f, 0f)));
            entryPositions.Add(new EntryInfo(Vector2Int.right, worldCenter + new Vector3(roomSize.x / 2f - 1.25f, 0f, 0f)));
            entryPositions.Add(new EntryInfo(Vector2Int.up, worldCenter + new Vector3(0f, roomSize.y / 2f - 0.75f, 0f)));
            entryPositions.Add(new EntryInfo(Vector2Int.down, worldCenter + new Vector3(0f, -roomSize.y / 2f + 0.75f, 0f)));
        }
    }

    public Vector3 GetCenterPosition()
    {
        Bounds bounds = tilemap.localBounds;
        if (roomType == ROOMTYPE.Long)
            return transform.position + new Vector3(bounds.size.x / 2f, 0f, 0f);
        return transform.position;
    }

    public Vector3 GetEntryPositionFrom(Vector2Int fromDirection)
    {
        Vector2Int opposite = -fromDirection;
        foreach (var entry in entryPositions)
        {
            if (entry.direction == opposite)
                return entry.position;
        }
        Debug.Log("None entry position");
        return GetCenterPosition();
    }


    public bool IsOccupyingPosition(Vector2 pos)
    {
        return occupiedPositions.Contains(pos);
    }

    public TilemapRenderer GetRoomBounds()
    {
        return roomBounds;
    }

    public static Vector3 GetWorldCenterOfTile(Vector2 gridPos, Vector2 roomSize)
    {
        float worldX = gridPos.x * roomSize.x;
        float worldY = gridPos.y * roomSize.y;
        return new Vector3(worldX, worldY, 0f);
    }

    public List<Vector2> GetAllOccupiedPositions()
    {
        return occupiedPositions;
    }
}
