using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public DungeonManager dungeonManager;
    public float checkCooldown = 0.2f;

    private bool isChecking = true;

    void Update()
    {
        if (isChecking)
        {
            CheckAndMove();
        }
    }
    void CheckAndMove()
    {
        Room current = dungeonManager.currentRoom;
        GameObject player = GameObject.FindWithTag("Player");

        if (current == null || player == null || current.tilemap == null) return;

        Vector3 pos = player.transform.position;
        Bounds bounds = current.tilemap.localBounds;
        Vector3 worldCenter = current.transform.position + bounds.center;
        Bounds worldBounds = new Bounds(worldCenter, bounds.size);

        Vector2Int dir = Vector2Int.zero;

        // ��踦 �Ѵ� ������ üũ
        if (pos.x > worldBounds.max.x) dir = Vector2Int.right;
        else if (pos.x < worldBounds.min.x) dir = Vector2Int.left;
        else if (pos.y > worldBounds.max.y) dir = Vector2Int.up;
        else if (pos.y < worldBounds.min.y) dir = Vector2Int.down;

        // ���� ��踦 �Ѿ��ٸ� �ٸ� ������ �̵� �õ�
        if (dir != Vector2Int.zero)
        {
            if (dungeonManager.TryMove(dir, out Room nextRoom))
            {
                isChecking = false;
                dungeonManager.navigator.MovePlayerToRoom(nextRoom, player, dir);
                StartCoroutine(ResetCheckCooldown());
            }
        }
    }

    IEnumerator ResetCheckCooldown()
    {
        yield return new WaitForSeconds(checkCooldown);
        isChecking = true;
    }

    public void TryMoveToNextRoom(Vector2Int direction)
    {
        if (dungeonManager.TryMove(direction, out Room nextRoom))
        {
            dungeonManager.navigator.MovePlayerToRoom(nextRoom, GameObject.FindWithTag("Player"), direction);
        }
    }

}
