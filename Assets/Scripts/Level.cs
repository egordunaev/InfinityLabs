using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public Room startRoom, endRoom;
    public Room[] possibleRooms, deadEnds;
    private List<Room> levelRooms = new List<Room>();
    public int MaximumRooms = 5;
    private System.Random random = new System.Random();
    private Room currentRoom, parentRoom;
    private int roomIndex;
    public Player player;
    public List<Transform> leftEndPoints;
    private bool isGenerated = false;
    public NavMeshSurface navigationSurface;
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
        navigationSurface.BuildNavMesh();
        ActivateSpawners(levelRooms);
        if (isGenerated)
        {
            SpawnPlayer();
        }
    }
    void ActivateSpawners(List<Room> rooms)
    {
        foreach(var room in rooms)
        {
            room.TurnOnSpawners();
        }
    }
    void GenerateLevel()
    {
        Debug.Log("Status:" + isGenerated.ToString());
        roomIndex = 0;
        PlaceRoom(startRoom);
        Room temp = null;
        parentRoom = currentRoom;
        while(roomIndex <= MaximumRooms)
        {
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
            roomIndex++;
            PlaceRest(possibleRooms);
        }
        roomIndex++;
        PlaceRoom(endRoom);
        Debug.Log("There are " + leftEndPoints.ToArray().Length + " exits remaining");
        PlaceRest(deadEnds);
        isGenerated = true;
        Debug.Log("Status:" + isGenerated.ToString());
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
                //MaximumRooms -= parentRoom.endPoint.Where(x => (x != point)).ToArray().Length;
            }
            currentRoom.transform.forward = point.forward;
            currentRoom.transform.position += point.position - currentRoom.startPoint.position;
            RoomCollisionCheck(currentRoom);
        }
        levelRooms.Add(currentRoom);
    }
    void PlaceRest(Room[] rooms)
    {
        while (leftEndPoints.ToArray().Length != 0)
        {
            foreach (var endPoint in leftEndPoints.ToList())
            {
                
                roomIndex++;
                Room tempRoom = null;
                //add collision check
                tempRoom = Instantiate(rooms[random.Next(0, rooms.Length)]) as Room;
                tempRoom.gameObject.name = "Room_" + roomIndex + "_" + tempRoom.roomType;
                tempRoom.transform.parent = transform;
                tempRoom.transform.forward = endPoint.forward;
                tempRoom.transform.position += endPoint.position - tempRoom.startPoint.position;
                RoomCollisionCheck(tempRoom);
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

        if (colliders.Length != 0)
        {
            Debug.Log("Collision");
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(120, 255, 20, 170);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
    void SpawnPlayer()
    {
        player = Instantiate(player) as Player;
        player.gameObject.name = "Player";
        player.transform.parent = transform;
        player.transform.forward = startRoom.playerSpawnPoint.forward;
        player.transform.position += startRoom.playerSpawnPoint.position;
    }
}
