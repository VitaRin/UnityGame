using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField]
    private float sightRange = 1f;

    [SerializeField]
    private LayerMask rayLayer;
    
    private Transform player;

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
        //AnimationUpdate();
    }

    public bool DetectPlayer()
    {
        if (player.position.x - transform.position.x > 0 && facingRight || player.position.x - transform.position.x < 0 && !facingRight)
        {
            Vector2 source = new Vector2(transform.position.x, transform.position.y + 0.5f);

            Vector2 target = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y -0.5f);

            
            
            RaycastHit2D hit = Physics2D.Raycast(source, target, sightRange);
            
            if (hit.collider != null && hit.collider.tag == "Player")
            {   
                //Debug.Log(hit.collider);
                Debug.DrawLine(transform.position, hit.point, Color.red);
                return true;
            }
        }
        return false;
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

