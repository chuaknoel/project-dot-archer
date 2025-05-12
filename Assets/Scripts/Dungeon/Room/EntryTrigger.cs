using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    public Vector2Int direction; // �̵� ����
    public Room parentRoom;      // Room�� �ܺο��� ���� �Ҵ� ����

    private void Start()
    {
        parentRoom = GetComponentInParent<Room>();
        if (parentRoom == null)
        {
            Debug.LogError("EntryTrigger: �θ� Room�� ã�� �� �����ϴ�.");
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
