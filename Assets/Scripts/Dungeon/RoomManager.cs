using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour // 방 클리어 여부, 적 목록, 입장처리
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

        // 맵 생성
        rooms = roomGenerator.GenerateDungeon();

        // 시작 위치 설정
        Room startRoom = FindRoomAtPosition(Vector2Int.zero);
        if (startRoom != null)
        {
            currentRoom = startRoom;
            currentRoom.isVisited = true; //처음방에서는 몬스터가 나오지 않아야 하기 때문에 방문한 방 체크를 미리 해준다.
            isCleared = true;
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

    public bool isCleared;

    public void OnPlayerEnter(Room visitRoom)
    {
        // EnemyManager에 몬스터 생성 요청
        isCleared = false;
        dungeonManager.player.SearchTarget.SetTarget(dungeonManager.enemyManager.SpawnEnemies(visitRoom));  // 이 방에 적을 생성하도록 요청후 적 리스트를 플레이어에게 전달
        dungeonManager.player.Controller.ChangeLook(true);
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        DungeonManager.Instance.UpgradeSelect.SetCard();
        // 문 열기 등 처리
    }
}
