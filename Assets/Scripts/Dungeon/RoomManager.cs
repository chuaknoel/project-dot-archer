using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour // �� Ŭ���� ����, �� ���, ����ó��
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    public Dictionary<Vector2Int, Room> rooms;
    public Room currentRoom;

    private DungeonManager dungeonManager;

    public void Init()
    {
        dungeonManager = DungeonManager.Instance;

        // �� ����
        rooms = roomGenerator.GenerateDungeon();

        // ���� ��ġ ����
        if (rooms.TryGetValue(Vector2Int.zero, out Room startRoom))
        {
            currentRoom = startRoom;
            currentRoom.isVisited = true; //ó���濡���� ���Ͱ� ������ �ʾƾ� �ϱ� ������ �湮�� �� üũ�� �̸� ���ش�.
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
        Vector2Int nextPos = currentRoom.position + direction;

        if (rooms.TryGetValue(nextPos, out newRoom))
        {
            currentRoom = newRoom;
            dungeonManager.cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            return true;
        }
        else
        {
            Debug.Log("���� ���� �������� �ʽ��ϴ�.");
        }

        return false;
    }

    public bool isCleared;

    public void OnPlayerEnter(Room visitRoom)
    {
        // EnemyManager�� ���� ���� ��û
        dungeonManager.player.SearchTarget.SetTarget(dungeonManager.enemyManager.SpawnEnemies(visitRoom));  // �� �濡 ���� �����ϵ��� ��û�� �� ����Ʈ�� �÷��̾�� ����
        dungeonManager.player.Controller.ChangeLook(true);
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // �� ���� �� ó��
    }
}
