using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTypes : MonoBehaviour
{
    public List<DoorTypes> doors = new List<DoorTypes>();
}

public enum DoorTypes
{
   TOP_DOOR,
   LEFT_DOOR,
   RIGHT_DOOR,
   BOTTOM_DOOR
}
