using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RoomManager : MonoBehaviour // �� Ŭ���� ����, �� ���, ����ó��
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    private List<Room> rooms = new();
    public List<Room> Rooms => rooms;
    public Room currentRoom;
    private Room bossroom;
    private DungeonManager dungeonManager;
    private Room room;
    private bool isBossRoomTriggered = false;

    public void Init()
    {
        dungeonManager = DungeonManager.Instance;
        room = Room.Instance;
        // �� ����
        rooms = roomGenerator.GenerateDungeon();

        // ���� ��ġ ����
        Room startRoom = FindRoomAtPosition(Vector2Int.zero);
        if (startRoom != null)
        {
            currentRoom = startRoom;
            currentRoom.isVisited = true; //ó���濡���� ���Ͱ� ������ �ʾƾ� �ϱ� ������ �湮�� �� üũ�� �̸� ���ش�.
            isCleared = true;
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
    public Room FindRoomAtPosition(Vector2 position)
    {
        return rooms.Find(r => r.IsOccupyingPosition(position));
    }

    public bool isCleared;

    public void OnPlayerEnter(Room visitRoom)
    {
        // EnemyManager�� ���� ���� ��û
        dungeonManager.player.SearchTarget.SetTarget(dungeonManager.enemyManager.SpawnEnemies(visitRoom));  // �� �濡 ���� �����ϵ��� ��û�� �� ����Ʈ�� �÷��̾�� ����
        dungeonManager.player.Controller.ChangeLook(true);
        isCleared = false;
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // �� ���� �� ó��
        if (!room.isBossRoom)
            CheckAllRoomsCleared();
    }

    public void CheckAllRoomsCleared()
    {
        //�Ϲ� �� �� Ŭ���� �� �� ���� ������ ����
        foreach (Room room in rooms)
        {
            if (!room.isBossRoom && !isCleared) return;
            bossroom = room;
        }
            
        if (!isBossRoomTriggered)
        {
            isBossRoomTriggered = true;
            StartCoroutine(AutoMoveToBossRoomAfterDelay(bossroom));
        }
    }

    private IEnumerator AutoMoveToBossRoomAfterDelay(Room room)
    {
        yield return new WaitForSeconds(3f);
        MovePlayerToBossRoom(room);
    }

    private void MovePlayerToBossRoom(Room room)
    {
        Player player = FindObjectOfType<Player>();

        Vector3 bossCenter = room.transform.position;
        player.transform.position = bossCenter;
    }
}
