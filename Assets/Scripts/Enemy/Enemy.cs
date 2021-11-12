using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    //COMPONENTS
    NavMeshAgent agent;
    Rigidbody rb;

    //Keep reference to the player or a target that the enemy should move towards
    public GameObject target;

    enum EnemyType { Chase, Patrol }
    [SerializeField] EnemyType enemyType;

    enum PatrolType { DistanceBased, TriggerBased }
    [SerializeField] PatrolType patrolType;

    //set true to automatically populate the path array
    //set false to set the path array manually in the inspector
    public bool autoGenPath;

    //Used to tell the agent on what path points to use (depending on the tag of the path point)
    public string pathName;

    //Array of pathNodes for patrol behaviour
    public GameObject[] path;

    //Keep track of our location in the path array
    public int pathIndex = 0;

    //Used to tell NavMesh Agent when to switch to the next path node
    public float distanceToNextNode;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        if (string.IsNullOrEmpty(pathName))
            pathName = "PatrolNode";

        if (distanceToNextNode <= 0)
            distanceToNextNode = 1.0f;

        if (!target && enemyType == EnemyType.Chase)
            target = GameObject.FindWithTag("Player");
        else if (enemyType == EnemyType.Patrol)
        {
            //Finds all pathnodes and adds them to the array.
            //If pathnodes already exist because they have been set in the inspector, they will be replaced
            if (autoGenPath)
                path = GameObject.FindGameObjectsWithTag(pathName);

            if (path.Length > 0)
                target = path[pathIndex];
        }

        //set agent to walk towards destination on first frame
        if (target)
            agent.SetDestination(target.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (target && enemyType == EnemyType.Patrol && patrolType == PatrolType.DistanceBased)
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.red);

            if (agent.remainingDistance < distanceToNextNode)
            {
                if (path.Length > 0)
                {
                    pathIndex++;

                    //method 1:
                    pathIndex %= path.Length;

                    //method 2:
                    //if (pathIndex >= path.Length)
                    //    pathIndex = 0;

                    target = path[pathIndex];
                }
            }
        }

        if (target)
            agent.SetDestination(target.transform.position);

        //Animations for things here
    }

    //Usage for this is to ensure we have a collider on our character and our rigidbody iskinematic
    //We can create trigger based movement to the next pathnode if we don't like the distance based solution in update
    private void OnTriggerEnter(Collider other)
    {
    }
}
