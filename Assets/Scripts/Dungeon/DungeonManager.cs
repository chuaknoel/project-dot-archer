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

    private List<Room> rooms = new();
    public List<Room> Rooms => rooms;
    public Room currentRoom;

    private CameraController cameraController;

    public Inventory inventory;
    public UpgradeManager upgradeManager;

    public List<GameObject> TestEnemies;

    public SkillManager skillManager;
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
        Room startRoom = FindRoomAtPosition(Vector2Int.zero);
        if (startRoom != null)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.GetComponent<IDefenceStat>().TakeDamage(123123);
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
            cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
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

    public void SetPlayerData()
    {
        PlayerData testData = new PlayerData
            (
                playerName: "sdf",
                statData: new StatData(new AttackStatData(), new MoveStatData(5f))
            );

        player.Init(testData, inventory);
    }
}
