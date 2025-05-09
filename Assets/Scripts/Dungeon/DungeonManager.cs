using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    public GameObject player;

    public Dictionary<Vector2Int, Room> rooms;
    public Room currentRoom;

    void Start()
    {   
        // �� ����
        rooms = roomGenerator.GenerateDungeon();

        // ���� ��ġ ����
        if (rooms.TryGetValue(Vector2Int.zero, out Room startRoom))
        {
            currentRoom = startRoom;
            navigator.MovePlayerToRoom(currentRoom, player, Vector2Int.zero); // �ʱ⿣ ���� ����
        }
        else
        {
            Debug.LogError("�ʱ� ��ġ(Vector2Int.zero)�� ���� �����ϴ�!");
        }
    }

    /// �̵� �õ� �� ��ȿ�� ��� true ��ȯ
    public bool TryMove(Vector2Int direction, out Room newRoom)
    {
        Vector2Int nextPos = currentRoom.position + direction;
        
        if (rooms.TryGetValue(nextPos, out newRoom))
        {
            currentRoom = newRoom;
            return true;
        }
        else
        {
            Debug.Log("���� ���� �������� �ʽ��ϴ�.");
        }

        return false;
    }
}
