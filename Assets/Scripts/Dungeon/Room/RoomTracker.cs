using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public RoomManager roomManager;
    public float checkCooldown = 0.2f;

    public bool isChecking = true;

    void Update()
    {
        if (isChecking)      //To EV : 문에 트리거를 만들어서 트리거 체크를 하면 업데이트가 필요 없지 않을까요? 나중에 다시 얘기해봐요!
        {
            CheckAndMove();
        }
    }

    void CheckAndMove()
    {
        Room current = roomManager.currentRoom;

        if (current == null || DungeonManager.Instance.player == null || current.tilemap == null) return;

        Vector3 pos = DungeonManager.Instance.player.transform.position;
        Bounds bounds = current.tilemap.localBounds;
        Vector3 worldCenter = current.transform.position + bounds.center;
        Bounds worldBounds = new Bounds(worldCenter, bounds.size);

        Vector2Int dir = Vector2Int.zero;

        // 경계를 넘는 방향을 체크
        if (pos.x > worldBounds.max.x) dir = Vector2Int.right;
        else if (pos.x < worldBounds.min.x) dir = Vector2Int.left;
        else if (pos.y > worldBounds.max.y) dir = Vector2Int.up;
        else if (pos.y < worldBounds.min.y) dir = Vector2Int.down;

        // 만약 경계를 넘었다면 다른 방으로 이동 시도
        if (dir != Vector2Int.zero)
        {
            if (roomManager.TryMove(dir, out Room nextRoom))
            {
                isChecking = false;
                roomManager.navigator.MovePlayerToRoom(nextRoom, DungeonManager.Instance.player.gameObject, dir);
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
        if (roomManager.TryMove(direction, out Room nextRoom))
        {
            roomManager.navigator.MovePlayerToRoom(nextRoom, DungeonManager.Instance.player.gameObject , direction);
        }
    }

}
