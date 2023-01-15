using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField]
    private float sightRange = 6f;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask rayLayer;
    
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
        currentPosition = transform.position;
        distance = Vector2.Distance(player.transform.position, transform.position);

        playerSpotted = DetectPlayer();
    }

    private bool DetectPlayer()
    {
        if (player.position.x - transform.position.x > 0 && facingRight || player.position.x - transform.position.x < 0 && !facingRight)
        {
            Vector2 source = new Vector2(transform.position.x, transform.position.y + 0.5f);

            Vector2 target = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y -0.5f);

            
            
            RaycastHit2D hit = Physics2D.Raycast(source, target, sightRange);

            //Debug.Log(hit.point);
            
            if (hit.collider != null && hit.collider.tag == "Player")
            {   
                Debug.Log(hit.collider);
                Debug.DrawLine(transform.position, hit.point, Color.red);
                return true;
            }
        }
        return false;
    }

    public bool PlayerSpotted()
    {
        return playerSpotted;
    }

    public void AnimationUpdate()
    {
        bool idle;
        float xDiff = transform.position.x - currentPosition.x;

        if (xDiff > 0f)
        {
            idle = false;
            sprite.flipX = false;
            facingRight = true;
        }
        else if (xDiff < 0f)
        {
            idle = false;
            sprite.flipX = true;
            facingRight = false;
        }
        else
        {
            idle = true;
        }

        anime.SetBool("Idle", idle);
    }
}

