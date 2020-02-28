using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour
{
    public RoomType roomType;
    // public GameObject propSpawnArea, monsterSpawnArea; // will figure out later 
    public Transform startPoint;
    public Transform[] endPoint;
    public Room parent, child = null; // to know info about previous and next rooms, null by default
    public Room[] possibleConnectingRooms;

    private System.Random random = new System.Random();
    public Collision collision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Room GetNextRoom(Room room)
    {
        Room selectedRoom = room.possibleConnectingRooms[random.Next(0, possibleConnectingRooms.Length)];
        selectedRoom.parent = this;
        child = selectedRoom;
        return selectedRoom;
    }
}

public enum RoomType
{
    RoomStart,
    RoomEnd, //used for end of the level
    RoomDeadEnd,
    TurnLeft,
    TurnRight,
    StairsUp,
    StairsDown,
    TIntersection,
    Straight
}
