using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    public EnemyAI enemyAI;

    [SerializeField]
    private float speed = 3f;

    public float nextWaypointDistance = 3f;
    public float jumpRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D enemy;

    public bool onSight = false;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();
    }

    public void Follow()
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
            enemyAI.AnimationUpdate();
        }
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

}
