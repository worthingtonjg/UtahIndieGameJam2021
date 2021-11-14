using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public Animator animator; 
    public GameObject EnemyPrefab;
    public GameObject BloodyMessPrefab;
    public float chaseSpeed = 4;
    public float patrolSpeed = 2;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private int currentWayPoint;
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
        Attacking,
        Patrolling,
        Eating,
    }

    private EnumState state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();

        sightlines = new List<GameObject>();
        foreach(Transform child in transform)
        {
            if(child.tag == "Sightline")
            {
                sightlines.Add(child.gameObject);
            }
        }

        if(waypoints.Count > 0)
        {
            currentWayPoint = Random.Range(0, waypoints.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(state == EnumState.Eating) 
        {
            animator.SetTrigger(Attack);
            return;
        }

        foreach(var sightline in sightlines)
        {
            var sightLineDirection = sightline.transform.position - transform.position;
            Debug.DrawRay(transform.position, sightLineDirection, Color.green);            
        }

        var playerSighted = false;
        
        foreach(var sightline in sightlines)
        {
            var sightLineDirection = sightline.transform.position - transform.position;
            RaycastHit sightLineHit;
            if (Physics.Raycast(transform.position, sightLineDirection, out sightLineHit))
            {
                if(sightLineHit.collider.gameObject == player)
                {
                    playerSighted = true;
                    state = EnumState.Chasing; 
                    break;
                }
            }
        }

        if(state == EnumState.Attacking && !playerSighted)
        {
            state = EnumState.Idle;
            canSeePlayer = false;
        }

        if(state == EnumState.Chasing)
        {
            animator.SetBool(IsWalking, true);
            var direction = (player.transform.position + Vector3.up) - transform.position;
            RaycastHit hit;
            canSeePlayer = false;
            Debug.DrawRay(transform.position, direction, Color.yellow);
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    canSeePlayer = true;
                    Debug.DrawRay(transform.position, direction, Color.red);

                    transform.LookAt(player.transform);

                    lastSeenAt = player.transform.position;

                    agent.stoppingDistance = 3;
                    agent.speed = chaseSpeed;                
                    agent.SetDestination(lastSeenAt.Value);
                    state = EnumState.Chasing;
                }
            }
        }

        if(canSeePlayer)
        {
            if(state == EnumState.Chasing)
            {
                if(agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = EnumState.Attacking;
                }
            }

            if(state == EnumState.Attacking)
            {
                animator.SetTrigger(Attack);
            }
        }
        else
        {
            if(state == EnumState.Chasing)
            {
                if(agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = EnumState.Idle;
                    animator.SetBool(IsWalking, false);
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
                    agent.speed = patrolSpeed;
                    agent.SetDestination(waypoints[currentWayPoint].transform.position);
                    animator.SetBool(IsWalking, true);
                }

                state = EnumState.Patrolling;
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(state != EnumState.Patrolling) return;
        if(!other.CompareTag("Waypoint")) return;

        var script = other.GetComponent<Waypoint>();
        if(script.body != null)
        {
            if(script.body.activeSelf)
            {
                agent.enabled = false;
                state = EnumState.Eating;
                script.body.SetActive(false);
                GameObject.Instantiate(BloodyMessPrefab, other.transform.position, Quaternion.identity);
                StartCoroutine("HatchEnemy", other.transform.position);
            }
        }
    }

    private IEnumerator HatchEnemy(Vector3 position)
    {
        yield return new WaitForSeconds(3f);
        agent.enabled = true;
        state = EnumState.Idle;
        GameObject.Instantiate(EnemyPrefab, position, Quaternion.identity);        
    }
}
