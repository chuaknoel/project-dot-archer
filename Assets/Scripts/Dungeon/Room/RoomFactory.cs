using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RoomFactory : MonoBehaviour
{
    [System.Serializable]
    public class RoomPrefabSet
    {
        public ROOMTYPE type;
        public GameObject prefab;
    }

    [SerializeField] private List<RoomPrefabSet> roomPrefabs;

    private Dictionary<ROOMTYPE, GameObject> prefabDict = new();

    private void Awake()
    {
        foreach (var set in roomPrefabs)
        {
            prefabDict[set.type] = set.prefab;
        }
    }

    public Vector2 roomSize = new Vector2(10f, 8f); // �� 1ĭ�� ���� ũ��

    // 1. �⺻ ��
    // 3. ������ (2x2)
    // centerPos: �߽� ��ǥ (4ĭ ��հ�), occupiedPositions: ���� �� ��ǥ ���
    public Room CreateRoom(Vector2 position, ROOMTYPE type)
    {
        if (type == ROOMTYPE.Long)
        {
            Debug.LogError("ROOMTYPE.Long�� ��ǥ 2���� �޾ƾ� �մϴ�. �����ε�� CreateRoom�� ����ϼ���.");
            return null;
        }
        return CreateRoomInternal(position, type, position);
    }

    // 2. �� ��
    public Room CreateLongRoom(Vector2 left, Vector2 right, ROOMTYPE type)
    {
        if (type != ROOMTYPE.Long)
        {
            Debug.LogWarning("ROOMTYPE.Long�� �ƴѵ� ��ǥ 2���� �����߽��ϴ�. ù ��ǥ�� �������� ����մϴ�.");
            return CreateRoomInternal(left, type, left);
        }
        Vector2 mid = (left + (Vector2)right) / 2f;
        return CreateRoomInternal(mid, type, left);
    }


    // ���� ���� ó�� �Լ�
    private Room CreateRoomInternal(Vector2 worldGridPos, ROOMTYPE type, Vector2 logicalPos)
    {
        if (!prefabDict.TryGetValue(type, out GameObject prefab))
        {
            Debug.LogError($"Prefab for room type {type} not found.");
            return null;
        }

        GameObject obj = Instantiate(prefab);
        obj.transform.position = new Vector3(worldGridPos.x, worldGridPos.y, 0f);

        Room room = obj.GetComponent<Room>();
        room.Init(worldGridPos, type, logicalPos);
        return room;
    }

}