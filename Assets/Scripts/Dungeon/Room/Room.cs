using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class Room : MonoBehaviour
{
    public Vector2Int position; // ���� ��ǥ (����� ���� ����)
    public List<Vector2Int> occupiedPositions = new(); // �� ���� �����ϴ� ��� ��ǥ

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
                Debug.LogError($"[Room] {name}: ������ Tilemap�� �����ϴ�.");
                return;
            }
        }

        Bounds bounds = tilemap.localBounds;
        Vector3 roomSize = bounds.size;

        if (roomType == ROOMTYPE.Long)
        {
            // ����� position�� ���� ����
            occupiedPositions.Add(position);
            occupiedPositions.Add(position + Vector2Int.right);
            name = $"Room_Long_{position.x}_{position.y}_to_{position.x + 1}_{position.y}";
        }
        else
        {
            occupiedPositions.Add(position);
            name = $"Room_{position.x}_{position.y}";
        }

        // �� ��ġ ���
        Vector3 roomWorldPosition;

        //if (roomType == ROOMTYPE.Long)
        //{
        //    // �� ���� ���� �����̹Ƿ�, ��ü bounds.size�� ���ݸ�ŭ�� �̵�
        //    roomWorldPosition = new Vector3(
        //        position.x * 9f,  // 1ĭ�� 18�� �̵�
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

        // ��ġ ����: tilemap�� pivot�� �߾��̸� bounds.center�� ����� ���� �������� �̵���
        transform.position = roomWorldPosition - bounds.center;

        Vector3 center = transform.position + bounds.center;
        entryPositions.Clear();

        if (roomType == ROOMTYPE.Long)
        {
            Vector3 leftRoomCenter = transform.position;
            Vector3 rightRoomCenter = transform.position + Vector3.right * (bounds.size.x / 2);

            // �¿� ��
            entryPositions[Vector2Int.left] = leftRoomCenter + new Vector3(-8.75f, 0f, 0f);
            entryPositions[Vector2Int.right] = rightRoomCenter + new Vector3(1.75f, 0f, 0f);

            // ���Ʒ� ��: �� �� ����, ��ǥ ��Ȯ�ϰ� ����
            entryPositions[new Vector2Int(-1, 1)] = new Vector3(transform.position.x, transform.position.y + bounds.max.y - 0.75f, 0f);           // ���� ��
            entryPositions[new Vector2Int(-1, -1)] = new Vector3(transform.position.x, transform.position.y + bounds.min.y + 0.75f, 0f);         // ���� �Ʒ�

            entryPositions[new Vector2Int(1, 1)] = rightRoomCenter + new Vector3(0, bounds.extents.y, 0f);  // ������ ��
            entryPositions[new Vector2Int(1, -1)] = rightRoomCenter + new Vector3(0, -bounds.extents.y, 0f); // ������ �Ʒ�
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