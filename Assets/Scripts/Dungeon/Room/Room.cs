using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class Room : MonoBehaviour
{
    public static DungeonManager Instance { get; private set; }

    public Vector2Int position;           // �� ���� Grid ��ǥ
    public Tilemap tilemap;              // Floor ���� Tilemap
    [SerializeField] private TilemapRenderer roomBounds;

    public bool isVisited = false;
    public ROOMTYPE roomType;

    private Dictionary<Vector2Int, Vector3> entryPositions = new();

    public void Init(Vector2Int pos, ROOMTYPE type)
    {
        position = pos;
        roomType = type;
        name = $"Room_{position.x}_{position.y}";

        // Floor Tilemap �ڵ� �Ҵ�
        if (tilemap == null)
        {
            tilemap = GetComponentInChildren<Tilemap>();
            if (tilemap == null)
            {
                Debug.LogError($"{name}: ������ Tilemap�� �������� �ʽ��ϴ�.");
                return;
            }
        }

        // Ÿ�ϸ� �ٿ�� ��� (���� ���� �� ���� ��ġ�� �ݿ� �ʿ�)
        Bounds bounds = tilemap.localBounds;
        Vector3 roomSize = bounds.size;

        // �� Room�� ���� ���� ��ġ ���
        Vector3 roomWorldPosition = new Vector3(
            position.x * roomSize.x,
            position.y * roomSize.y,
            0f
        );
        transform.position = roomWorldPosition;

        // ���� �߽� ��� (���� ��ǥ ����)
        Vector3 center = transform.position + bounds.center;

        // �Ա� ��ġ ��� (���� Ÿ�ϸ� ����)
        entryPositions.Clear();

        // �� �Ա��� ���� ��ǥ�� �°� ����
        if (roomType == ROOMTYPE.Long)
        {
            // ���: �¿� ���� 2ĭ. �¿� 1��, ���Ʒ� �� 2�� �Ա� ����
            float halfX = roomSize.x / 2f;
            float leftCenterX = transform.position.x + bounds.min.x + halfX / 2f;
            float rightCenterX = transform.position.x + bounds.min.x + halfX + halfX / 2f;

            // �¿�
            entryPositions[Vector2Int.left] = new Vector3(transform.position.x + bounds.min.x + 1.25f, center.y, 0f);
            entryPositions[Vector2Int.right] = new Vector3(transform.position.x + bounds.max.x - 1.25f, center.y, 0f);

            // �� 2��
            entryPositions[Vector2Int.up + Vector2Int.left] = new Vector3(leftCenterX, transform.position.y + bounds.max.y - 0.75f, 0f);
            entryPositions[Vector2Int.up + Vector2Int.right] = new Vector3(rightCenterX, transform.position.y + bounds.max.y - 0.75f, 0f);

            // �Ʒ� 2��
            entryPositions[Vector2Int.down + Vector2Int.left] = new Vector3(leftCenterX, transform.position.y + bounds.min.y + 0.75f, 0f);
            entryPositions[Vector2Int.down + Vector2Int.right] = new Vector3(rightCenterX, transform.position.y + bounds.min.y + 0.75f, 0f);
        }
        else
        {
            // �Ϲݹ�
            entryPositions[Vector2Int.left] = new Vector3(transform.position.x + bounds.min.x + 1.25f, center.y, 0f);
            entryPositions[Vector2Int.right] = new Vector3(transform.position.x + bounds.max.x - 1.25f, center.y, 0f);
            entryPositions[Vector2Int.up] = new Vector3(center.x, transform.position.y + bounds.max.y - 0.75f, 0f);
            entryPositions[Vector2Int.down] = new Vector3(center.x, transform.position.y + bounds.min.y + 0.750f, 0f);
        }
    }
    public Vector3 GetCenterPosition()
    {
        return transform.position; // �Ǵ� bounds.center �� ���� �߽� ��ġ ��ȯ
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
