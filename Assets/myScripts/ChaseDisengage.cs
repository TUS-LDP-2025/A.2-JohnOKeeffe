using UnityEngine;
using UnityEngine.AI;

public class ChaseDisengage : MonoBehaviour
{
    private Material npcMatt;
    private NavMeshAgent agent;
    private Patrol patrol;
    public Transform player;

    public bool isChasing;

    private float patrolSpeed;
    [SerializeField] private float chaseSpeed = 5;

    [SerializeField] private float chaseStartDistance;
    [SerializeField] private float chaseGiveUpDistance;

    //Public events that will be invoked in this script to call the methods "StartPatrol" and "StopPatrol" in the Patrol script.

    public event System.Action OnStartChase;
    public event System.Action OnEndChase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        patrol = GetComponent<Patrol>();

        npcMatt = GetComponent<MeshRenderer>().material;

        //Subscribing functions to events.
        OnStartChase += patrol.StopPatrol;
        OnEndChase += patrol.StartPatrol;

        patrolSpeed = agent.speed;

        npcMatt.color = Color.limeGreen;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);


        //Starts and ends chase depending on distance player is from agent and if "isChasing" is true
        if (!isChasing && distanceFromPlayer <= chaseStartDistance)
        {
            StartChase();
        } else if (isChasing && distanceFromPlayer >= chaseGiveUpDistance)
        {
            EndChase();
        }

        if(isChasing)
        {
            agent.SetDestination(player.position);
        }
    }

    //When OnStartChase is invoked in the this script, "StopPatrol" will be called so the agent can chase the player.
    void StartChase()
    {
        agent.speed = chaseSpeed;
        npcMatt.color = Color.red;

        isChasing = true;
        OnStartChase?.Invoke();
    }

    //When OnEndchase is invoked, "StartPatrol" will be called so agent can resume patrolling.
    void EndChase()
    {
        agent.speed = patrolSpeed;
        npcMatt.color = Color.limeGreen;

        isChasing = false;
        OnEndChase?.Invoke();

    }

    //Unsubscribing the functions if the gameObject is destroyed.
    private void OnDestroy()
    {
        OnStartChase -= patrol.StopPatrol;
        OnEndChase -= patrol.StartPatrol;
    }

}
