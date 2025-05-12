using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class Room : MonoBehaviour
{
    public static DungeonManager Instance { get; private set; }

    public Vector2Int position;           // 이 방의 Grid 좌표
    public Tilemap tilemap;              // Floor 기준 Tilemap
    [SerializeField] private TilemapRenderer roomBounds;

    public bool isVisited = false;
    public ROOMTYPE roomType;

    private Dictionary<Vector2Int, Vector3> entryPositions = new();

    public void Init(Vector2Int pos, ROOMTYPE type)
    {
        position = pos;
        roomType = type;
        name = $"Room_{position.x}_{position.y}";

        // Floor Tilemap 자동 할당
        if (tilemap == null)
        {
            tilemap = GetComponentInChildren<Tilemap>();
            if (tilemap == null)
            {
                Debug.LogError($"{name}: 하위에 Tilemap이 존재하지 않습니다.");
                return;
            }
        }

        // 타일맵 바운드 계산 (로컬 기준 → 월드 위치에 반영 필요)
        Bounds bounds = tilemap.localBounds;
        Vector3 roomSize = bounds.size;

        // 이 Room의 실제 월드 위치 계산
        Vector3 roomWorldPosition = new Vector3(
            position.x * roomSize.x,
            position.y * roomSize.y,
            0f
        );
        transform.position = roomWorldPosition;

        // 방의 중심 계산 (월드 좌표 기준)
        Vector3 center = transform.position + bounds.center;

        // 입구 위치 계산 (방의 타일맵 기준)
        entryPositions.Clear();

        // 각 입구를 월드 좌표에 맞게 조정
        if (roomType == ROOMTYPE.Long)
        {
            // 긴방: 좌우 길이 2칸. 좌우 1개, 위아래 각 2개 입구 설정
            float halfX = roomSize.x / 2f;
            float leftCenterX = transform.position.x + bounds.min.x + halfX / 2f;
            float rightCenterX = transform.position.x + bounds.min.x + halfX + halfX / 2f;

            // 좌우
            entryPositions[Vector2Int.left] = new Vector3(transform.position.x + bounds.min.x + 1.25f, center.y, 0f);
            entryPositions[Vector2Int.right] = new Vector3(transform.position.x + bounds.max.x - 1.25f, center.y, 0f);

            // 위 2개
            entryPositions[Vector2Int.up + Vector2Int.left] = new Vector3(leftCenterX, transform.position.y + bounds.max.y - 0.75f, 0f);
            entryPositions[Vector2Int.up + Vector2Int.right] = new Vector3(rightCenterX, transform.position.y + bounds.max.y - 0.75f, 0f);

            // 아래 2개
            entryPositions[Vector2Int.down + Vector2Int.left] = new Vector3(leftCenterX, transform.position.y + bounds.min.y + 0.75f, 0f);
            entryPositions[Vector2Int.down + Vector2Int.right] = new Vector3(rightCenterX, transform.position.y + bounds.min.y + 0.75f, 0f);
        }
        else
        {
            // 일반방
            entryPositions[Vector2Int.left] = new Vector3(transform.position.x + bounds.min.x + 1.25f, center.y, 0f);
            entryPositions[Vector2Int.right] = new Vector3(transform.position.x + bounds.max.x - 1.25f, center.y, 0f);
            entryPositions[Vector2Int.up] = new Vector3(center.x, transform.position.y + bounds.max.y - 0.75f, 0f);
            entryPositions[Vector2Int.down] = new Vector3(center.x, transform.position.y + bounds.min.y + 0.750f, 0f);
        }
    }
    public Vector3 GetCenterPosition()
    {
        return transform.position; // 또는 bounds.center 등 실제 중심 위치 반환
    }
    public Vector3 GetEntryPositionFrom(Vector2Int fromDirection)
    {
        Vector2Int oppositeDir = -fromDirection;
        return entryPositions.ContainsKey(oppositeDir) ? entryPositions[oppositeDir] : transform.position;
    }

    public TilemapRenderer GetRoomBounds()
    {
        return roomBounds;
    }
}
