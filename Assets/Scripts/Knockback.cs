using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private float duration = 1f;
    public bool beingKnockedDown = false;

    public void Feedback(Vector2 direction, float force)
    {
        beingKnockedDown = true;
        rb.AddForce(new Vector2(direction.x * -5, direction.y) * force, ForceMode2D.Impulse);
        rb.velocity = direction * force;

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(duration);
        beingKnockedDown = false;
    }

}
