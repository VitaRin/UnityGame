using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    private int currentWaypointIndex = 0;

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
    }

    void FixedUpdate()
    {
        if (enemyAI.DetectPlayer() == false)
        {
            Invoke("UpdatePath", 0f);    
        }

        enemyAI.AnimationUpdate();

    }

    void UpdatePath()
    {
        destinationSetter.target = waypoints[currentWaypointIndex].transform;
        target = destinationSetter.target;

        if (seeker.IsDone())
        {
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

                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            path = p;
        }
        
    }
}
