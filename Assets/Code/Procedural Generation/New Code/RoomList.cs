using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="RoomList", menuName="Procedural Generation/Room List")]
public class RoomList : ScriptableObject
{
    public RoomTag tag;
    public GameObject[] rooms;
}
