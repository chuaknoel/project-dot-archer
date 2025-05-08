using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public Room currentRoom;

    public void SetRoom(Room room)
    {
        currentRoom = room;
    }
}
