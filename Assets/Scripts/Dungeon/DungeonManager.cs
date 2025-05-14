using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class DungeonManager : MonoBehaviour //방 이동, 전체 흐름 등 맵 전체 책임자
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
            Debug.LogWarning("여러 개의 DungeonManager 인스턴스가 존재합니다. 하나만 유지해야 합니다.");
            Destroy(gameObject); // 혹은 중복 방지 처리
        }
    }

    void Start()
    {
        // 맵 생성
        rooms = roomGenerator.GenerateDungeon();
        cameraController = Camera.main.GetComponent<CameraController>();
        inventory.EquipSelectedItems(); //게임매니저 생기면 그떄 조절

        // 시작 위치 설정
        Room startRoom = FindRoomAtPosition(Vector2Int.zero);
        if (startRoom != null)
        {
            currentRoom = startRoom;
            cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            navigator.MovePlayerToRoom(currentRoom, player.gameObject, Vector2Int.zero); // 초기엔 방향 없음
            SetPlayerData();
        }
        else
        {
            Debug.LogError("초기 위치(Vector2Int.zero)에 방이 없습니다!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.GetComponent<IDefenceStat>().TakeDamage(123123);
        }
    }

    /// 이동 시도 후 유효한 경우 true 반환
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

        Debug.Log("다음 방이 존재하지 않습니다.");
        return false;
    }

    /// 좌표에 해당하는 방을 리스트에서 직접 찾는 메서드
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
