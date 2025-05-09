using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigator : MonoBehaviour
{
    [SerializeField] private RoomGenerator roomGenerator;

    /// ���⿡ ���� ����� ������ �÷��̾ �̵�
    public void MovePlayerToRoom(Room room, GameObject player, Vector2Int fromDirection)
    {
        Vector3 entry = room.GetEntryPositionFrom(fromDirection);
        Vector3 offset = ((Vector2)fromDirection).normalized * 1.0f;
        Vector3 targetPos = entry + offset;

        StartCoroutine(MovePlayerWithCollisionPause(player, targetPos));
        room.isVisited = true;
    }

    /// Ÿ�� ��ġ�� �ִ� ������ �÷��̾ �̵�
    /// ���� ������ ���� ��� �⺻ �߽����� �̵�
    public void MovePlayerToRoomByPosition(Vector2Int targetPosition, GameObject player)
    {
        if (roomGenerator.Rooms.TryGetValue(targetPosition, out Room targetRoom))
        {
            Vector2Int fromDirection = -GetDirectionToRoom(player.transform.position, targetRoom);
            MovePlayerToRoom(targetRoom, player, fromDirection);
        }
        else
        {
            Debug.LogWarning("�̵��Ϸ��� ���� �������� �ʽ��ϴ�: " + targetPosition);
        }
    }

    /// �÷��̾��� ���� ��ġ���� Ÿ�� ������� ������ ����
    /// (�ܼ��� �߽� ��ġ ��)
    private Vector2Int GetDirectionToRoom(Vector3 playerPos, Room targetRoom)
    {
        Vector3 roomCenter = targetRoom.GetCenterPosition();
        Vector2Int direction = Vector2Int.zero;

        if (Mathf.Abs(roomCenter.x - playerPos.x) > Mathf.Abs(roomCenter.y - playerPos.y))
            direction = roomCenter.x > playerPos.x ? Vector2Int.right : Vector2Int.left;
        else
            direction = roomCenter.y > playerPos.y ? Vector2Int.up : Vector2Int.down;

        return direction;
    }

    private IEnumerator MovePlayerWithCollisionPause(GameObject player, Vector3 targetPosition)
    {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        player.transform.position = targetPosition;

        yield return new WaitForSeconds(0.2f); // 0.2�� �� �浹 ����

        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }
}
