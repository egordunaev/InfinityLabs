using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Room[] startRoom, endRoom, possibleRooms;
    public int MaximumRooms = 5;
    private System.Random random = new System.Random();
    private Room currentRoom, parentRoom;
    private int roomIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {
        GenerateLevel();
    }
    void GenerateLevel()
    {
        PlaceRoom(startRoom[0]);
        Room temp = null;
        parentRoom = currentRoom;
        for (int item = 0; item <= MaximumRooms; item++)
        {
            roomIndex = item;
            bool forWhileLoop = true;
            while (forWhileLoop)
            {
                //tries to find good room without collision
                if (parentRoom.possibleConnectingRooms.Length != 0)
                {
                    temp = parentRoom.GetNextRoom(parentRoom);
                    //if (!RoomCollisionCheck(temp))
                    //{
                        forWhileLoop = false;
                    //}
                }
                
            }
            PlaceRoom(temp);
            parentRoom = currentRoom;
        }
        PlaceRoom(endRoom[0]);

    }
    void PlaceRoom(Room room)
    {
        currentRoom = Instantiate(room) as Room;
        currentRoom.gameObject.name = "Room_" + roomIndex + "_" + currentRoom.roomType;
        currentRoom.transform.parent = transform;
        if (parentRoom)
        {
            var point = parentRoom.endPoint[random.Next(0, parentRoom.endPoint.Length)]; // picking random exit point
            currentRoom.transform.forward = point.forward;
            currentRoom.transform.position += point.position - currentRoom.startPoint.position;
        }

    }
    bool RoomCollisionCheck(Room room)
    {
        Debug.Log("this");
        if (room)
        {
            if (!room.collision.gameObject)
            {
                Debug.Log("");
                return false;
            }
        }
        
        return true;
    }
}
