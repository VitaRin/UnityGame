using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.20f;

    [SerializeField]
    private float jumpForce = 14f;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private AudioSource jumpSound;

    int jumps = 0;

    private enum MovementState { idle, running, jumping, falling }

    private Rigidbody2D player;
    private BoxCollider2D col;
    private Animator anime;
    private SpriteRenderer sprite;
    public Knockback knockback;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player.bodyType != RigidbodyType2D.Static && knockback.beingKnockedDown == false)
        {
            Vector2 velocity = player.velocity;

            if (Input.GetKey(KeyCode.A))
            {
                velocity.x = -speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                velocity.x = speed;
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumps < 1)
            {
                jumpSound.Play();
                velocity.y = jumpForce;
                jumps += 1;
            }
            if (IsGrounded())
            {
                jumps = 0;
            }
            
            player.velocity = velocity;
        }
        UpdateAnimation();
    }

    private bool IsGrounded()
    {

        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    private void UpdateAnimation()
    {
        MovementState state;
        float direction = Input.GetAxisRaw("Horizontal");

        if (direction > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (direction < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (player.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        if (player.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anime.SetInteger("State", (int)state);
    }
}
