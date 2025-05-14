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


    /// 좌표에 해당하는 방을 리스트에서 직접 찾는 메서드
    public Room FindRoomAtPosition(Vector2 position)
    {
        return rooms.Find(r => r.IsOccupyingPosition(position));
    }




    public bool isCleared;

    public void OnPlayerEnter()
    {
        if (!isCleared)
        {
            // EnemyManager에 몬스터 생성 요청
            EnemyManager.Instance.SpawnEnemies(this);  // 이 방에 적을 생성하도록 요청
        }
    }

    public void OnAllEnemiesDefeated()
    {
        isCleared = true;
        // 문 열기 등 처리
    }
}
