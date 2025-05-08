using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    public GameObject player;

    private Dictionary<Vector2Int, Room> rooms;
    private Room currentRoom;

    void Start()
    {
        Debug.Log("DungeonManager Start Called");

        rooms = roomGenerator.GenerateDungeon();
        currentRoom = rooms[Vector2Int.zero];
        navigator.MovePlayerToRoom(currentRoom, player);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) TryMove(Vector2Int.up);
        else if (Input.GetKeyDown(KeyCode.S)) TryMove(Vector2Int.down);
        else if (Input.GetKeyDown(KeyCode.A)) TryMove(Vector2Int.left);
        else if (Input.GetKeyDown(KeyCode.D)) TryMove(Vector2Int.right);
    }

    void TryMove(Vector2Int dir)
    {
        Vector2Int nextPos = currentRoom.position + dir;
        if (rooms.TryGetValue(nextPos, out Room nextRoom))
        {
            currentRoom = nextRoom;
            navigator.MovePlayerToRoom(currentRoom, player);
        }
    }


}
