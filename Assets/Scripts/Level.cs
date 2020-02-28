using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Room[] startRoom, endRoom, possibleRooms;
    public int MaximumRooms = 5;
    private System.Random random = new System.Random();
    private Room currentRoom, parentRoom;
    private int roomIndex;
    public List<Transform> leftEndPoints;
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
        roomIndex = 0;
        PlaceRoom(startRoom[0]);
        Room temp = null;
        parentRoom = currentRoom;
        for (int item = 1; item <= MaximumRooms; item++)
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
        roomIndex++;
        PlaceRoom(endRoom[0]);
        Debug.Log("There are " + leftEndPoints.ToArray().Length + " exits remaining");
        PlaceRest();
    }
    void PlaceRoom(Room room)
    {
        currentRoom = Instantiate(room) as Room;
        currentRoom.gameObject.name = "Room_" + roomIndex + "_" + currentRoom.roomType;
        currentRoom.transform.parent = transform;
        if (parentRoom)
        {
            var point = parentRoom.endPoint[random.Next(0, parentRoom.endPoint.Length)]; // picking random exit point
            if (parentRoom.endPoint.Length > 1)
            {
                leftEndPoints.AddRange(parentRoom.endPoint.Where(x => (x != point)).ToArray());
                MaximumRooms -= parentRoom.endPoint.Where(x => (x != point)).ToArray().Length;
            }
            currentRoom.transform.forward = point.forward;
            currentRoom.transform.position += point.position - currentRoom.startPoint.position;
        }

    }
    void PlaceRest()
    {
        while (leftEndPoints.ToArray().Length != 0)
        {
            foreach (var endPoint in leftEndPoints.ToList())
            {
                
                roomIndex++;
                Room tempRoom = null;
                //add collision check
                tempRoom = Instantiate(possibleRooms[random.Next(0, possibleRooms.Length)]) as Room;
                tempRoom.gameObject.name = "Room_" + roomIndex + "_" + tempRoom.roomType;
                tempRoom.transform.parent = transform;
                tempRoom.transform.forward = endPoint.forward;
                tempRoom.transform.position += endPoint.position - tempRoom.startPoint.position;
                //add collision check
                if (tempRoom.roomType == RoomType.RoomDeadEnd)
                {
                    leftEndPoints.Remove(endPoint);
                }
                else
                {
                    leftEndPoints.AddRange(tempRoom.endPoint);
                    leftEndPoints.Remove(endPoint);
                }
            }
            Debug.Log("There are " + leftEndPoints.ToArray().Length + " exits remaining");
        }
    }
    bool RoomCollisionCheck(Room room)
    {
        Collider[] colliders = Physics.OverlapBox(room.transform.position, room.transform.localScale);
        if(colliders.Length != 0) { }
        return false;
    }
}
