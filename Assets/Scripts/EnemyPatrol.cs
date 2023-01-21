using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    private int currentWaypointIndex = 0;
    
    private int currentWaypoint = 0;

    private float startWaitTime = 1f;

    private float waitTime;

    public EnemyAI enemyAI;

    private Seeker seeker;

    private Path path;

    private Rigidbody2D enemy;

    private Transform target;

    private float nextWaypointDistance = 0.5f;

    private bool pathComplete = false;


    void Start()
    {
        waitTime = startWaitTime;
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();
    }

    public void Patrol()
    {
        Invoke("UpdatePath", 0f);

        if (path != null)
        {
            if (currentWaypoint < path.vectorPath.Count)
            {        
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemy.position).normalized;
                Vector2 force = new Vector2(direction.x * 2f, 0f) * 1000f * Time.deltaTime;

                enemy.AddForce(force);

                float distance = Vector2.Distance(enemy.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                pathComplete = true;
            }
        }     

        enemyAI.AnimationUpdate();
    }

    void UpdatePath()
    {
        target = waypoints[currentWaypointIndex].transform;

        if (seeker.IsDone())
        {
            seeker.StartPath(enemy.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            if (pathComplete)
            {
                if (waitTime <= 0)
                {
                    currentWaypointIndex++;
                    waitTime = startWaitTime;
                    pathComplete = false;
                    currentWaypoint = 0;

                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                    }
                    
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            path = p;
        }
        
    }
}
