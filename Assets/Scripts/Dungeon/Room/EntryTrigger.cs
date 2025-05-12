using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    public Vector2Int direction; // 이동 방향
    public Room parentRoom;      // Room을 외부에서 직접 할당 받음

    private void Start()
    {
        parentRoom = GetComponentInParent<Room>();
        if (parentRoom == null)
        {
            Debug.LogError("EntryTrigger: 부모 Room을 찾을 수 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || parentRoom == null) return;

        Vector2Int targetRoomPos = parentRoom.position + direction;
        RoomNavigator navigator = FindObjectOfType<RoomNavigator>();
        navigator?.MovePlayerToRoomByPosition(targetRoomPos, other.gameObject);
    }
}
