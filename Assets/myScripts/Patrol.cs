using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

    private NavMeshAgent agent;

    public List<Transform> patrolPoints;

    public bool canPatrol = true;

    int currentPoint = 0;

    private float patrolSpeed = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(patrolPoints[currentPoint].position);

        patrolSpeed = agent.speed;


    }

    // Update is called once per frame
    void Update()
    {
        
    Patrolling();

    }

    void Patrolling()
    {
        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPoint].position);
        Debug.Log($"Going to {currentPoint}");

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log($"Arrived at {currentPoint} ");

            currentPoint = (currentPoint + 1) % patrolPoints.Count;

        }

    }

}
