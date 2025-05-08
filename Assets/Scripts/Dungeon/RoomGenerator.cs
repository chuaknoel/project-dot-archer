using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public int maxRooms = 10;

    private Dictionary<Vector2Int, Room> rooms = new();
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

        return rooms;
    }

    private void CreateRoom(Vector2Int position, Enums.ROOMTYPE type)
    {
        Debug.Log($"Creating Room at {position}");

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

}
