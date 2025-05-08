using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigator : MonoBehaviour
{
    public void MovePlayerToRoom(Room room, GameObject player)
    {
        player.transform.position = room.transform.position;
        room.isVisited = true;
    }
}
