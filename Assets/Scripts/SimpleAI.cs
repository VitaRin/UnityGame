using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{   
    [SerializeField]
    private float speed;

    [SerializeField]
    private float sight;

    [SerializeField]
    private float minDistance;
    
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer > sight)
        {
            
        }
        else if (distanceFromPlayer < sight && distanceFromPlayer > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
