using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class DungeonManager : MonoBehaviour //�� �̵�, ��ü �帧 �� �� ��ü å����
{
    public static DungeonManager Instance { get; private set; }

    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    public Player player;
    public EnemyManager enemyManager;

    public Dictionary<Vector2Int, Room> rooms;
    public Room currentRoom;

    private CameraController cameraController;

    public Inventory inventory;

    public List<GameObject> TestEnemies;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("���� ���� DungeonManager �ν��Ͻ��� �����մϴ�. �ϳ��� �����ؾ� �մϴ�.");
            Destroy(gameObject); // Ȥ�� �ߺ� ���� ó��
        }
    }

    void Start()
    {
        // �� ����
        rooms = roomGenerator.GenerateDungeon();
        cameraController = Camera.main.GetComponent<CameraController>();
        inventory.EquipSelectedItems(); //���ӸŴ��� ����� �׋� ����

        // ���� ��ġ ����
        if (rooms.TryGetValue(Vector2Int.zero, out Room startRoom))
        {
            currentRoom = startRoom;
            cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            navigator.MovePlayerToRoom(currentRoom, player.gameObject, Vector2Int.zero); // �ʱ⿣ ���� ����
            SetPlayerData();
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
            newRoom.GetComponent<RoomManager>().OnPlayerEnter();  // �� �̵� �� �� ����
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

        player.Init(testData,inventory);
    }
}
