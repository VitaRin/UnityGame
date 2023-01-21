using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.7f;
    
    private Rigidbody2D rb;

    private EnemyAttack enemyAttack;

    // [SerializeField]
    // private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyAttack = enemy.GetComponent<EnemyAttack>();
        rb.velocity = enemyAttack.fireDir * speed;
        //enemyAttack.shooting = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log(collision.name);
        }
        //enemyAttack.shooting = false;
    }
}
