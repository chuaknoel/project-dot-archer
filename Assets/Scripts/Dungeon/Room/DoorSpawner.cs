using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public void SpawnDoors(Dictionary<Vector2Int, Room> rooms)
    {
        foreach (var kvp in rooms)
        {
            Vector2Int pos = kvp.Key;
            Room room = kvp.Value;

            foreach (var dir in directions)
            {
                Vector2Int neighbor = pos + dir;
                if (!rooms.ContainsKey(neighbor)) continue;

                Vector3 spawnPos = room.GetEntryPositionFrom(-dir);
                GameObject door = Instantiate(doorPrefab, spawnPos, Quaternion.identity, room.transform);

                if (dir == Vector2Int.left)
                    door.transform.rotation = Quaternion.Euler(0, 0, 90);
                else if (dir == Vector2Int.right)
                    door.transform.rotation = Quaternion.Euler(0, 0, -90);
                else if (dir == Vector2Int.down)
                    door.transform.rotation = Quaternion.Euler(0, 0, 180);
                else
                    door.transform.rotation = Quaternion.identity;

                door.GetComponent<EntryTrigger>().direction = dir;
            }
        }
    }
}
