using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField]
    private float sightRange = 5f;
    
    private Transform player;

    public bool playerSpotted = false;
    
    private Animator anime;

    private Rigidbody2D enemy;

    private SpriteRenderer sprite;
    
    private Vector2 currentPosition;

    public bool facingRight = true;

    [SerializeField]
    private EnemyPatrol enemyPatrol;

    [SerializeField]
    public EnemyAttack enemyAttack;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (DetectPlayer() || enemyAttack.InRange())
        {
            enemyAttack.Follow();
        }
        else
        {
            enemyPatrol.Patrol();
        }
    }

    private bool DetectPlayer()
    {
        if ((player.position.x - transform.position.x) > 0 && facingRight || (player.position.x - transform.position.x) < 0 && !facingRight)
        {
            Vector2 source = new Vector2(transform.position.x, transform.position.y + 0.5f);

            Vector2 target = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y - 0.5f);

            
            
            RaycastHit2D hit = Physics2D.Raycast(source, target, sightRange);
            
            if (hit.collider != null && hit.collider.tag == "Player")
            {   
                //Debug.DrawLine(transform.position, hit.point, Color.red);
                enemyAttack.onSight = true;
                return true;
            }
        }
        return false;
    }

    public void AnimationUpdate()
    {
        bool idle;

        //Debug.Log(enemy.velocity);

        if (enemy.velocity.x >= 0.05f)
        {
            idle = false;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingRight = true;
        }
        else if (enemy.velocity.x <= -0.05f)
        {
            idle = false;
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }
        else
        {
            idle = true;
        }

        anime.SetBool("Idle", idle);
    }
}

