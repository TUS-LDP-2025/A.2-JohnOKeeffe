using UnityEngine;
using UnityEngine.AI;

public class ChaseDisengage : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);


    }
}
