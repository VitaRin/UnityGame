using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    [SerializeField]
    private float speed = 1f;

    private int currentWaypointIndex = 0;

    private float startWaitTime = 3f;

    private float waitTime;

    public EnemyAI enemyAI;


    void Start()
    {
        waitTime = startWaitTime;
        
    }

    void Update()
    {
        if (enemyAI.PlayerSpotted() == false)
        {
                               
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
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

            }
            else 
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            }   
        }

        enemyAI.AnimationUpdate();

    }

}
