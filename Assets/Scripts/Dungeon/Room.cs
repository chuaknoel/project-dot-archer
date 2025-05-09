using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class Room : MonoBehaviour
{
    public int roomID;
    public Vector2Int position;           // �� ���� Grid ��ǥ
    public ROOMTYPE roomType;
    public Tilemap tilemap;              // Floor ���� Tilemap
    public bool isVisited = false;

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
        entryPositions[Vector2Int.left] = new Vector3(transform.position.x + bounds.min.x + 1f, center.y, 0f);
        entryPositions[Vector2Int.right] = new Vector3(transform.position.x + bounds.max.x - 1f, center.y, 0f);
        entryPositions[Vector2Int.up] = new Vector3(center.x, transform.position.y + bounds.max.y - 1.5f, 0f);
        entryPositions[Vector2Int.down] = new Vector3(center.x, transform.position.y + bounds.min.y + 1f, 0f);
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
}
