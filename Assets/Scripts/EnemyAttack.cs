using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    
    private Transform target;

    public EnemyAI enemyAI;

    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private Transform firepoint;

    [SerializeField]
    private LayerMask wall;

    [SerializeField]
    private AudioSource fireSound;

    [SerializeField]
    private float speed = 3f;

    public float nextWaypointDistance = 3f;
    public float jumpRequirement = 0.8f;
    public float jumpCheckOffset = 0.1f;

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D enemy;

    private float sightRange = 5f;

    public bool onSight = false;

    public bool shooting = false;

    public Vector2 fireDir;

    private float timerStart = 1.5f;

    private float timer;

    private bool seesPlayer;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();
        target = player.transform;
    }

    public void Follow()
    {   
        seesPlayer = LookForPlayer();
        
            if (seesPlayer)
            {

                Shoot();
            }
            else
            {
                Invoke("UpdatePath", 0f);

                if (path != null)
                {
                    if (currentWaypoint < path.vectorPath.Count)
                    {
                        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
                        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
                        
                        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemy.position).normalized;
                        Vector2 force = new Vector2(direction.x * 2f, 0f) * speed * Time.deltaTime;

                        if (direction.y > jumpRequirement && isGrounded)
                        {
                            enemy.AddForce(new Vector2(0f, direction.y) * speed * 2.5f);
                        }
                        else if (direction.y < -0.5f && isGrounded)
                        {
                            enemy.AddForce(new Vector2(direction.x, 0f) * 10f);
                        }

                        enemy.AddForce(force);

                        float distance = Vector2.Distance(enemy.position, path.vectorPath[currentWaypoint]);
                        if (distance < nextWaypointDistance)
                        {
                            currentWaypoint++;
                        }
                    }
                    else
                    {
                        currentWaypoint = 0;
                    }
                    
                }
            }
        enemyAI.AnimationUpdate();
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(enemy.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
        }
    }

    public bool InRange()
    {
        // If the enemy sees the player, it starts following them whether they're in sight or not
        // as long as the player is in range.

        if (onSight)
        {
            if (Vector2.Distance(transform.position, target.transform.position) < 10f)
            {
                return true;
            }
            else
            {
                onSight = false;
            }
        }
        return false;
    }

    private bool LookForPlayer()
    {
        // Have a fixed aim where to fire the fireball.

        Vector2 source = new Vector2(transform.position.x, transform.position.y + 0.5f);

        Vector2 destination = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y - 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(source, destination, sightRange);
            
        if (hit.collider != null && hit.collider.tag == "Player")
        {   
            Debug.DrawLine(transform.position, hit.point, Color.red);
            fireDir = destination.normalized;
            return true;
        }
        return false;
    }

    private void Shoot()
    {
        if (!shooting)
        {
            Instantiate(fireball, firepoint.position, firepoint.rotation);
            timer = timerStart;
            shooting = true;
            fireSound.Play();
        }
        else
        {
            // Delay to avoid fireball spam.

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                shooting = false;
            }
        }
    }

    public void Aim()
    {
        // Aim for a random wall where to teleport the player.

        Vector2 source = new Vector2(transform.position.x, transform.position.y + 0.5f);

        Vector2 destination = new Vector2(Random.Range(-17, 35), Random.Range(-1, 21));

        RaycastHit2D hit = Physics2D.Raycast(source, destination, 40f, wall);

        if (hit.collider != null && hit.collider.tag == "Walls")
        {   
            Debug.DrawLine(transform.position, hit.point, Color.blue);
            
            // To avoid being stuck in walls.
            Vector2 tpDestination = hit.point - ((hit.point - (Vector2)player.transform.position).normalized * 2f);
            Debug.Log(hit.point);
            Debug.Log(tpDestination);
            player.GetComponent<PlayerCollide>().tpPos = tpDestination;
        }
    }
}
