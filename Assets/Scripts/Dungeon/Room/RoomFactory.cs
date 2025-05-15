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

    public Vector2 roomSize = new Vector2(10f, 8f); // 방 1칸의 월드 크기

    // 1. 기본 방
    // 3. 보스방 (2x2)
    // centerPos: 중심 좌표 (4칸 평균값), occupiedPositions: 실제 논리 좌표 목록
    public Room CreateRoom(Vector2 position, ROOMTYPE type)
    {
        if (type == ROOMTYPE.Long)
        {
            Debug.LogError("ROOMTYPE.Long은 좌표 2개를 받아야 합니다. 오버로드된 CreateRoom을 사용하세요.");
            return null;
        }
        return CreateRoomInternal(position, type, position);
    }

    // 2. 긴 방
    public Room CreateLongRoom(Vector2 left, Vector2 right, ROOMTYPE type)
    {
        if (type != ROOMTYPE.Long)
        {
            Debug.LogWarning("ROOMTYPE.Long이 아닌데 좌표 2개를 전달했습니다. 첫 좌표를 기준으로 사용합니다.");
            return CreateRoomInternal(left, type, left);
        }
        Vector2 mid = (left + (Vector2)right) / 2f;
        return CreateRoomInternal(mid, type, left);
    }


    // 내부 공통 처리 함수
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