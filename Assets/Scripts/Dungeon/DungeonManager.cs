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
        // 맵 생성
        rooms = roomGenerator.GenerateDungeon();
        cameraController = Camera.main.GetComponent<CameraController>();

        //테스트 매서드
        SetPlayerData();

        // 시작 위치 설정
        if (rooms.TryGetValue(Vector2Int.zero, out Room startRoom))
        {
            currentRoom = startRoom;
            cameraController.SetCameraBounds(currentRoom.GetRoomBounds());
            navigator.MovePlayerToRoom(currentRoom, player.gameObject, Vector2Int.zero); // 초기엔 방향 없음
            player.SearchTarget.SetTarget(roomEnemies);
        }
        else
        {
            Debug.LogError("초기 위치(Vector2Int.zero)에 방이 없습니다!");
        }
    }

    /// 이동 시도 후 유효한 경우 true 반환
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
            Debug.Log("다음 방이 존재하지 않습니다.");
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
