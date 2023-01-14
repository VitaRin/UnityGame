using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float sightRange = 3f;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float attackRange;
    
    private Transform player;

    private float distance;

    private bool seePlayer = false;

    private bool playerInRange = false;

    private bool playerInAttackRange = false;

    public bool attackMode = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        Debug.Log(distance);

        if (distance < sightRange)
        {
            // playerInRange = true;
            // Vector2 shoot = new Vector2(transform.position.x - 1f, transform.position.y);
            // RaycastHit2D hit = Physics2D.Raycast(shoot, player.transform.position, sightRange);
            // if (hit.collider != null)
            // {
            //     Debug.DrawLine(transform.position, hit.point, Color.red);
            // }
            attackMode = true;
        }
        else 
        {
            attackMode = false;
        }
    
        // else if (distance > -30f)
        // {
        //     playerInRange = true;
        //     Vector2 shoot = new Vector2(transform.position.x - 1f, transform.position.y);
        //     RaycastHit2D hit = Physics2D.Raycast(shoot, player.transform.position, sight);
        //     if (hit.collider != null)
        //     {
        //         Debug.DrawLine(transform.position, hit.point, Color.red);
        //     }
        //}

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position, sight);
        // if (hit.collider != null)
        // {
        //     Debug.DrawLine(transform.position, hit.point, Color.red);
        // }
        // float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        // if (distanceFromPlayer > sight)
        // {
            
        // }
        // else if (distanceFromPlayer < sight && distanceFromPlayer > minDistance)
        // {
        //     transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        // }
    }

    private void DetectPlayer()
    {

    }
    
    public bool PlayerSpotted()
    {
        return attackMode;
    }
}

