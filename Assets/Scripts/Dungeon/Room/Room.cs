using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class Room : MonoBehaviour
{
    public Vector2Int position; // 기준 좌표 (긴방은 왼쪽 기준)
    public List<Vector2Int> occupiedPositions = new(); // 이 방이 차지하는 모든 좌표

    public Tilemap tilemap;
    [SerializeField] private TilemapRenderer roomBounds;

    public bool isVisited = false;
    public ROOMTYPE roomType;

    private Dictionary<Vector2Int, Vector3> entryPositions = new();

    public void Init(Vector2Int pos, ROOMTYPE type)
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
        Vector3 roomSize = bounds.size;

        if (roomType == ROOMTYPE.Long)
        {
            // 긴방은 position이 왼쪽 기준
            occupiedPositions.Add(position);
            occupiedPositions.Add(position + Vector2Int.right);
            name = $"Room_Long_{position.x}_{position.y}_to_{position.x + 1}_{position.y}";
        }
        else
        {
            occupiedPositions.Add(position);
            name = $"Room_{position.x}_{position.y}";
        }

        // 방 위치 계산
        Vector3 roomWorldPosition;

        //if (roomType == ROOMTYPE.Long)
        //{
        //    // 긴 방은 왼쪽 기준이므로, 전체 bounds.size의 절반만큼만 이동
        //    roomWorldPosition = new Vector3(
        //        position.x * 9f,  // 1칸당 18씩 이동
        //        position.y * bounds.size.y,
        //        0f
        //    );
        //}
        //else
        {
            roomWorldPosition = new Vector3(
                position.x * bounds.size.x,
                position.y * bounds.size.y,
                0f
            );
        }

        // 위치 보정: tilemap의 pivot이 중앙이면 bounds.center를 빼줘야 원점 기준으로 이동됨
        transform.position = roomWorldPosition - bounds.center;

        Vector3 center = transform.position + bounds.center;
        entryPositions.Clear();

        if (roomType == ROOMTYPE.Long)
        {
            Vector3 leftRoomCenter = transform.position;
            Vector3 rightRoomCenter = transform.position + Vector3.right * (bounds.size.x / 2);

            // 좌우 문
            entryPositions[Vector2Int.left] = leftRoomCenter + new Vector3(-8.75f, 0f, 0f);
            entryPositions[Vector2Int.right] = rightRoomCenter + new Vector3(1.75f, 0f, 0f);

            // 위아래 문: 각 방 기준, 좌표 명확하게 구분
            entryPositions[new Vector2Int(-1, 1)] = new Vector3(transform.position.x, transform.position.y + bounds.max.y - 0.75f, 0f);           // 왼쪽 위
            entryPositions[new Vector2Int(-1, -1)] = new Vector3(transform.position.x, transform.position.y + bounds.min.y + 0.75f, 0f);         // 왼쪽 아래

            entryPositions[new Vector2Int(1, 1)] = rightRoomCenter + new Vector3(0, bounds.extents.y, 0f);  // 오른쪽 위
            entryPositions[new Vector2Int(1, -1)] = rightRoomCenter + new Vector3(0, -bounds.extents.y, 0f); // 오른쪽 아래
        }
        else
        {
            entryPositions[Vector2Int.left] = new Vector3(transform.position.x + bounds.min.x + 1.25f, center.y, 0f);
            entryPositions[Vector2Int.right] = new Vector3(transform.position.x + bounds.max.x - 1.25f, center.y, 0f);
            entryPositions[Vector2Int.up] = new Vector3(center.x, transform.position.y + bounds.max.y - 0.75f, 0f);
            entryPositions[Vector2Int.down] = new Vector3(center.x, transform.position.y + bounds.min.y + 0.75f, 0f);
        }
    }

    public Vector3 GetCenterPosition()
    {
        if (roomType == ROOMTYPE.Long)
        {
            Bounds bounds = tilemap.localBounds;
            return transform.position + new Vector3(bounds.size.x / 2f, 0f, 0f);
        }
        return transform.position;
    }

    public Vector3 GetEntryPositionFrom(Vector2Int fromDirection)
    {
        Vector2Int opposite = -fromDirection;
        return entryPositions.ContainsKey(opposite) ? entryPositions[opposite] : GetCenterPosition();
    }

    public bool IsOccupyingPosition(Vector2Int pos)
    {
        return occupiedPositions.Contains(pos);
    }

    public TilemapRenderer GetRoomBounds()
    {
        return roomBounds;
    }
}