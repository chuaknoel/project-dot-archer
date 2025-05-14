using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour // �� Ŭ���� ����, �� ���, ����ó��
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    private List<Room> rooms = new();
    public List<Room> Rooms => rooms;
    public Room currentRoom;

    private DungeonManager dungeonManager;

    public void Init()
    {
        dungeonManager = DungeonManager.Instance;

        // �� ����
        rooms = roomGenerator.GenerateDungeon();

        // ���� ��ġ ����
        Room startRoom = FindRoomAtPosition(Vector2Int.zero);
        if (startRoom != null)
        {
            currentRoom = startRoom;
            dungeonManager.cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            navigator.MovePlayerToRoom(currentRoom, dungeonManager.player.gameObject, Vector2Int.zero); // �ʱ⿣ ���� ����
        }
        else
        {
            Debug.LogError("�ʱ� ��ġ(Vector2Int.zero)�� ���� �����ϴ�!");
        }

    }

    /// �̵� �õ� �� ��ȿ�� ��� true ��ȯ
    public bool TryMove(Vector2Int direction, out Room newRoom)
    {
        Vector2 nextPos = currentRoom.position + direction;

        newRoom = FindRoomAtPosition(nextPos);
        if (newRoom != null)
        {
            currentRoom = newRoom;
            dungeonManager.cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            return true;
        }

        Debug.Log("���� ���� �������� �ʽ��ϴ�.");
        return false;

    }


    /// ��ǥ�� �ش��ϴ� ���� ����Ʈ���� ���� ã�� �޼���
    public Room FindRoomAtPosition(Vector2 position)
    {
        return rooms.Find(r => r.IsOccupyingPosition(position));
    }




    public bool isCleared;

    public void OnPlayerEnter()
    {
        if (!isCleared)
        {
            // EnemyManager�� ���� ���� ��û
            EnemyManager.Instance.SpawnEnemies(this);  // �� �濡 ���� �����ϵ��� ��û
        }
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // �� ���� �� ó��
    }
}
