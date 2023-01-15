using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    [SerializeField]
    private float speed = 3f;

    private int currentWaypointIndex = 0;

    private int currentWaypoint = 0;

    private float nextWaypointDistance = 0.5f;

    private bool reachedEnd = false;

    private bool waiting = false;

    private float startWaitTime = 1f;

    private float waitTime;

    public EnemyAI enemyAI;

    private Seeker seeker;

    private Path path;

    private Rigidbody2D enemy;

    private AIDestinationSetter destinationSetter;

    private Transform target;


    void Start()
    {
        waitTime = startWaitTime;
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        target = waypoints[currentWaypointIndex].transform;

        
        //seeker.StartPath(transform.position, target.position, OnPathComplete);
        
    }

    void FixedUpdate()
    {
        if (enemyAI.DetectPlayer() == false)
        {
            //if (waiting == false)
            //{
            Invoke("UpdatePath", 0f);
                // if (reachedEnd == true)
                // {
                //     return;
                // }
                
            //}
            

            if (path != null)
            {
                // Debug.Log(currentWaypoint);
                // Debug.Log(path.vectorPath.Count);
                int totalWaypoints = path.vectorPath.Count;
                if (currentWaypoint >= totalWaypoints)
                {
                    reachedEnd = true;
                    return;
                } 
                else
                {
                    reachedEnd = false;
                }
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemy.position).normalized;

                float distance = Vector2.Distance(enemy.position, path.vectorPath[currentWaypointIndex]);
                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                return;
            }
            
        }

        enemyAI.AnimationUpdate();

    }

    void UpdatePath()
    {
        destinationSetter.target = waypoints[currentWaypointIndex].transform;
        target = destinationSetter.target;

        if (seeker.IsDone())
        {
            //reachedEnd = true;
            seeker.StartPath(enemy.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            
            if (waitTime <= 0)
            {
                currentWaypointIndex++;
                waitTime = startWaitTime;
                waiting = false;

                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                
            }
            else
            {
                waitTime -= Time.deltaTime;
                waiting = true;
                // Debug.Log(waitTime);
            }
            path = p;
            currentWaypoint = 0;
        }
        
    }

    

}
