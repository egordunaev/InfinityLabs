using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public Player player;
    public float viewDistance;
    public Collider hearing;
    public Collider vision;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance: " + distance);
        if (distance<viewDistance)
        {

            meshAgent.SetDestination(player.transform.position);
            //Debug.Log("Going to: " + player.transform.position);
        }
    }
}
