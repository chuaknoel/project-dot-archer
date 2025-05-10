using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonManager : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    public Player player;
    public List<GameObject> roomEnemies;

    public Dictionary<Vector2Int, Room> rooms;
    public Room currentRoom;

    private CameraController cameraController;


    void Start()
    {   
        // �� ����
        rooms = roomGenerator.GenerateDungeon();
        cameraController = Camera.main.GetComponent<CameraController>();

        //�׽�Ʈ �ż���
        SetPlayerData();

        // ���� ��ġ ����
        if (rooms.TryGetValue(Vector2Int.zero, out Room startRoom))
        {
            currentRoom = startRoom;
            cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            navigator.MovePlayerToRoom(currentRoom, player.gameObject, Vector2Int.zero); // �ʱ⿣ ���� ����
            player.SearchTarget.SetTarget(roomEnemies);
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
            cameraController.SetCameraBounds(currentRoom.GetRoomBounds());

            return true;
        }
        else
        {
            Debug.Log("���� ���� �������� �ʽ��ϴ�.");
        }

        return false;
    }

    public void SetPlayerData()
    {
        PlayerData testData = new PlayerData
            (
                playerName: "sdf",
                statData: new StatData(new AttackStatData(), new MoveStatData(5f))
                
            );

        player.Init(testData);
    }
}
