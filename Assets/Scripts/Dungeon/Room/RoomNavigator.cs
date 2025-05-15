using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigator : MonoBehaviour
{
    [SerializeField] private RoomGenerator roomGenerator;

    public EnemyManager enemyManager;
    
    /// Ÿ�� ��ġ�� �ִ� ������ �÷��̾ �̵�
    /// ���� ������ ���� ��� �⺻ �߽����� �̵�
    public void MovePlayerToRoomByPosition(Vector2 targetPosition, GameObject player)
    {
        Room targetRoom = roomGenerator.Rooms.Find(r => r.IsOccupyingPosition(targetPosition));
        if (targetRoom != null)
        {
            Vector2Int fromDirection = GetDirectionToRoom(player.transform.position, targetRoom);
            MovePlayerToRoom(targetRoom, player, fromDirection);
        }
        else
        {
            Debug.LogWarning("�̵��Ϸ��� ���� �������� �ʽ��ϴ�: " + targetPosition);
        }
    }

    /// ���⿡ ���� ����� ������ �÷��̾ �̵�
    public void MovePlayerToRoom(Room room, GameObject player, Vector2Int fromDirection)
    {
        Vector3 entry = room.GetEntryPositionFrom(fromDirection);
        Vector3 offset = ((Vector2)fromDirection).normalized * 1.5f;
        Vector3 targetPos = entry + offset;

        if (room.isVisited == false)
        {
            Debug.Log("ó�� ���� ��?");
            //��� ���Ա� ����

            DungeonManager.Instance.roomManager.OnPlayerEnter(room);  //�̵��� ���� ó�� �� ���̸� Enemy ����
        }

        StartCoroutine(MovePlayerWithCollisionPause(player, targetPos));
        room.isVisited = true;
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

        yield return new WaitForSeconds(1f); // 1�� �� �浹 ����

        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }
}
