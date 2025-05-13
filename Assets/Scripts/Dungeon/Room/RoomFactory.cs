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

    public Vector2 roomSize = new Vector2(10f, 8f); // �� 1ĭ�� ���� ũ�� (����)

    public Room CreateRoom(Vector2Int position, ROOMTYPE type)
    {
        if (!prefabDict.TryGetValue(type, out GameObject prefab))
        {
            Debug.LogError($"Prefab for room type {type} not found.");
            return null;
        }

        GameObject obj = Instantiate(prefab);

        // === ��ġ ���� �߰� ===
        Vector3 worldPos = new Vector3(position.x * roomSize.x, position.y * roomSize.y, 0f);
        obj.transform.position = worldPos;

        Room room = obj.GetComponent<Room>();
        room.Init(position, type);
        return room;
    }
}