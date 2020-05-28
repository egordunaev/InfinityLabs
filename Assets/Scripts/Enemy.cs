using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator enemyAnimator;
    public NavMeshAgent meshAgent;
    public float attackDistance;
    public GameObject player;
    public float viewDistance;
    public float health = 100f;

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f)
        {
            meshAgent.isStopped = true;
            EnemyStateControl("Dead");
            return;
        }
        meshAgent.isStopped = false;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance: " + distance);
        if (distance <= attackDistance)
        {
            Attack();
        }
        if (distance<viewDistance)
        {
            EnemyStateControl("Walk");
            meshAgent.SetDestination(player.transform.position);
            //Debug.Log("Going to: " + player.transform.position);
        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnimator = GetComponent<Animator>();
        EnemyStateControl("IdleState");
        
    }
    void EnemyStateControl(string stateName)
    {
        enemyAnimator.SetTrigger(stateName);
    }
    void Attack()
    {
        meshAgent.isStopped = true;
        EnemyStateControl("Attack");
        
    }
}
