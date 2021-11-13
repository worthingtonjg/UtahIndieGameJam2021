using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public int currentWayPoint;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3? lastSeenAt;
    private bool canSeePlayer;
    private List<GameObject> waypoints;
    
    private List<GameObject> sightlines;

    private enum EnumState
    {
        Idle,
        Chasing,
        Patrolling,
    }

    private EnumState state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();
        sightlines = GameObject.FindGameObjectsWithTag("Sightline").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var sightline in sightlines)
        {
            Vector3 sightLineDirection = sightline.transform.position - transform.position;
            Debug.DrawRay(transform.position, sightLineDirection, Color.green);            
        }

        foreach(var sightline in sightlines)
        {
            Vector3 sightLineDirection = sightline.transform.position - transform.position;
            RaycastHit sightLineHit;
            if (Physics.Raycast(transform.position, sightLineDirection, out sightLineHit))
            {
                if(sightLineHit.collider.gameObject == player)
                {
                    state = EnumState.Chasing;
                    break;
                }
            }
        }

        if(state == EnumState.Chasing)
        {
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit hit;
            canSeePlayer = false;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    canSeePlayer = true;
                    Debug.DrawRay(transform.position, direction, Color.red);

                    transform.LookAt(player.transform);

                    lastSeenAt = player.transform.position;

                    agent.stoppingDistance = 3;
                    agent.speed = 3;                
                    agent.SetDestination(lastSeenAt.Value);
                    state = EnumState.Chasing;
                }
            }
        }

        if(!canSeePlayer)
        {
            if(state == EnumState.Chasing)
            {
                if(agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = EnumState.Idle;
                }
            }

            if(state == EnumState.Idle)
            {
                if(waypoints.Count > 0)
                {
                    ++currentWayPoint;
                    if(currentWayPoint >= waypoints.Count)
                    {
                        currentWayPoint = 0;
                    }

                    print($"Next waypoint: {currentWayPoint}");
                    agent.stoppingDistance = 0;
                    agent.speed = 1;
                    agent.SetDestination(waypoints[currentWayPoint].transform.position);
                }

                state = EnumState.Patrolling;
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(state == EnumState.Chasing) return;
        if(other.tag != "Waypoint") return;

        print($"arrived at waypoint");
        if(state == EnumState.Patrolling)
        {
            state = EnumState.Idle;
        }
    }
}