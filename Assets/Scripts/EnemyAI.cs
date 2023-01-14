using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
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

    public bool playerSpotted = false;
    
    private Animator anime;

    private Rigidbody2D enemy;

    private SpriteRenderer sprite;
    
    private Vector2 currentPosition;

    private bool facingRight = true;

    // private float facingWay;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        currentPosition = enemy.transform.position;
        distance = Vector2.Distance(player.transform.position, transform.position);
        //Debug.Log(distance);

        if (distance < sightRange)
        {
            
            // Vector2 shoot = new Vector2(transform.position.x - 1f, transform.position.y);
            // RaycastHit2D hit = Physics2D.Raycast(shoot, player.transform.position, sightRange);
            // if (hit.collider != null)
            // {
            //     Debug.DrawLine(transform.position, hit.point, Color.red);
            // }
            playerSpotted = true;
        }
        else 
        {
            playerSpotted = false;
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
        //AnimationUpdate();
    }

    private void DetectPlayer()
    {
        // Vector2 shoot = new Vector2(transform.position.x - 1f, transform.position.y);
            // RaycastHit2D hit = Physics2D.Raycast(shoot, player.transform.position, sightRange);
            // if (hit.collider != null)
            // {
            //     Debug.DrawLine(transform.position, hit.point, Color.red);
            // }
    }

    public bool PlayerSpotted()
    {
        return playerSpotted;
    }

    public void AnimationUpdate()
    {
        bool idle;
        float xDiff = enemy.transform.position.x - currentPosition.x;

        if (xDiff > 0f)
        {
            idle = false;
            sprite.flipX = false;
        }
        else if (xDiff < 0f)
        {
            idle = false;
            sprite.flipX = true;
        }
        else
        {
            idle = true;
        }

        anime.SetBool("Idle", idle);
    }
}

