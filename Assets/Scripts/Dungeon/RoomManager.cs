using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RoomManager : MonoBehaviour // 방 클리어 여부, 적 목록, 입장처리
{
    public RoomGenerator roomGenerator;
    public RoomNavigator navigator;
    private List<Room> rooms = new();
    public List<Room> Rooms => rooms;
    public Room currentRoom;
    private Room bossroom;
    private DungeonManager dungeonManager;
    private bool isBossRoomTriggered = false;
    public int currentStage = 1;

    public static RoomManager Instance;
    private void Awake() => Instance = this;

    public void Init()
    {
        dungeonManager = DungeonManager.Instance;
        // 맵 생성
        rooms = roomGenerator.GenerateDungeon(1);

        // 시작 위치 설정
        Room startRoom = FindRoomAtPosition(Vector2Int.zero);
        if (startRoom != null)
        {
            currentRoom = startRoom;
            currentRoom.isVisited = true; //처음방에서는 몬스터가 나오지 않아야 하기 때문에 방문한 방 체크를 미리 해준다.

            currentRoom.isCleared = true;

            dungeonManager.cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            navigator.MovePlayerToRoom(currentRoom, dungeonManager.player.gameObject, Vector2Int.zero); // 초기엔 방향 없음
        }
        else
        {
            Debug.LogError("초기 위치(Vector2Int.zero)에 방이 없습니다!");
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
            dungeonManager.cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            return true;
        }

        Debug.Log("다음 방이 존재하지 않습니다.");
        return false;

    }
    public Room FindRoomAtPosition(Vector2 position)
    {
        return rooms.Find(r => r.IsOccupyingPosition(position));
    }

    public void OnPlayerEnter(Room visitRoom)
    {
        // EnemyManager에 몬스터 생성 요청
        isCleared = false;
        dungeonManager.player.SearchTarget.SetTarget(dungeonManager.enemyManager.SpawnEnemies(visitRoom));  // 이 방에 적을 생성하도록 요청후 적 리스트를 플레이어에게 전달
        dungeonManager.player.Controller.ChangeLook(true);
        visitRoom.isVisited = true;
        visitRoom.isCleared = false;
    }

    public void OnAllEnemiesDefeated()
    {

        DungeonManager.Instance.UpgradeSelect.SetCard();

        currentRoom.isCleared = true;

        if (!currentRoom.isBossRoom)
            CheckAllRoomsCleared();
    }
    public void OnBossDefeated()
    {
        StartCoroutine(NextStageRoutine());
    }
    private IEnumerator NextStageRoutine()
    {
        yield return new WaitForSeconds(3f); // 연출용

        currentStage++;
        roomGenerator.GenerateStage(currentStage);

        // 플레이어 초기화 또는 이동
        Player player = FindObjectOfType<Player>();
        player.transform.position = Vector3.zero; // 시작 위치로 이동
    }

    public void CheckAllRoomsCleared()
    {
        //일반 방 중 클리어 안 된 방이 있으면 종료
        foreach (Room room in rooms)
        {
            if (!room.isBossRoom && !room.isCleared) return;
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