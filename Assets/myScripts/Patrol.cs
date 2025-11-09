using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    private NavMeshAgent agent;

    public List<Transform> patrolPoints;

    public bool isPatrolling;

    int currentPoint = 0;

    private float patrolSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(patrolPoints[currentPoint].position);
  
        ChaseDisengage chase = GetComponent<ChaseDisengage>();

        //Agent starts patrolling
        isPatrolling = true;

        StartCoroutine(Patrolling());
        patrolSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            Patrolling();
        }
    }

    //Returns the agent's speed to it's default speed and resumes patrolling.
    public void StartPatrol()
    {
        agent.speed = patrolSpeed;
        StartCoroutine(Patrolling());

        isPatrolling = true;
    }

    //Sets the agent's destination between patrol patrols and waits 0.5 second between points.
    private IEnumerator Patrolling()
    {
        while (true) { 
        agent.SetDestination(patrolPoints[currentPoint].position);

            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
            yield return new WaitForSeconds(0.5f); // Wait for 1 second before moving to the next point
            currentPoint = (currentPoint + 1) % patrolPoints.Count;

        }
    }

    //Stop the patrolling and resets the path so another destiation (the player) can be set.
    public void StopPatrol()
    {
        isPatrolling = false;
        agent.ResetPath();
    }
   

}
