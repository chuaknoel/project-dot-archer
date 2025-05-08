using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int position;
    public Enums.ROOMTYPE roomType;
    public bool isVisited = false;

    public int roomID;

    public void Init(Vector2Int pos, Enums.ROOMTYPE type)
    {
        position = pos;
        roomType = type;
        name = $"Room_{position.x}_{position.y}";
        transform.position = new Vector3(position.x * 20, position.y * 20, 0);
    }
}
