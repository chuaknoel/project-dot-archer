using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    [SerializeField] private GameObject doorPrefab; // Inspector���� Door ������ ����
    public int maxRooms = 10;
    
    private Dictionary<Vector2Int, Room> rooms = new();
    public Dictionary<Vector2Int, Room> Rooms => rooms;
    private Vector2Int[] directions = {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    public Dictionary<Vector2Int, Room> GenerateDungeon()
    {
        Vector2Int currentPos = Vector2Int.zero;
        CreateRoom(currentPos, Enums.ROOMTYPE.Normal);

        for (int i = 1; i < maxRooms; i++)
        {
            Vector2Int newPos;
            do
            {
                Vector2Int dir = directions[Random.Range(0, directions.Length)];
                newPos = currentPos + dir;
            } while (rooms.ContainsKey(newPos));

            CreateRoom(newPos, i == maxRooms - 1 ? Enums.ROOMTYPE.Boss : Enums.ROOMTYPE.Normal);
            currentPos = newPos;
        }

        // ��ü �濡 ���� �� ����
        GenerateDoorsForAllRooms();

        return rooms;
    }

    private void CreateRoom(Vector2Int position, Enums.ROOMTYPE type)
    {
        GameObject roomObj = Instantiate(roomPrefab);
        Room room = roomObj.GetComponent<Room>();

        if (room == null)
        {
            Debug.LogError("Room component not found on prefab!");
            return;
        }

        room.Init(position, type);
        rooms.Add(position, room);
    }

    private void GenerateDoorsForAllRooms()
    {
        foreach (var roomPair in rooms)
        {
            Room room = roomPair.Value;

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighborPos = room.position + dir;

                // �̿� ���� ������ ��쿡�� �� ����
                if (rooms.ContainsKey(neighborPos))
                {
                    CreateDoorAt(room, dir);
                }
            }
        }
    }

    private void CreateDoorAt(Room room, Vector2Int direction)
    {
        if (doorPrefab == null)
        {
            Debug.LogError("Door Prefab�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // ���� ���� �Ա� ��ġ ��� (�ݴ� ���⿡�� ����)
        Vector3 doorPos = room.GetEntryPositionFrom(-direction);
        //Quaternion rotation = Quaternion.identity;

        GameObject door = Instantiate(doorPrefab, doorPos, Quaternion.identity, room.transform);

        // ȸ�� �� ������ ����
        if (direction == Vector2Int.left)
        {
            //rotation = Quaternion.Euler(0, 0, 180);
            door.transform.localScale = new Vector3(0.5f, 1, 1);
        }
        else if (direction == Vector2Int.right)
        {
            //rotation = Quaternion.Euler(0, 0, 0);
            door.transform.localScale = new Vector3(0.5f, 1, 1);
        }
        else if (direction == Vector2Int.up)
        {
            //rotation = Quaternion.Euler(0, 0, 90);
            door.transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else if (direction == Vector2Int.down)
        {
            //rotation = Quaternion.Euler(0, 0, -90);
            door.transform.localScale = new Vector3(1, 0.5f, 1);
        }
        door.name = $"Door_{direction}_From_{room.name}";

        door.GetComponent<EntryTrigger>().direction = direction;
    }
}
