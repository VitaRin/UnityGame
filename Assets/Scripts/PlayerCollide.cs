using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollide : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator anime;

    [SerializeField]
    private AudioSource deathSound;

    [SerializeField]
    private Text pointsText;

    [SerializeField]
    private AudioSource cherrySound;   
    
    public bool hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Cherries: " + GlobalControl.Instance.cherries;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            GlobalControl.Instance.HP--;
            Die();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            cherrySound.Play();
            Destroy(collision.gameObject);
            GlobalControl.Instance.cherries++;
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            hasKey = true;
        }
    }

    private void Die()
    {
        deathSound.Play();
        player.bodyType = RigidbodyType2D.Static;
        anime.SetTrigger("Death");
        GlobalControl.Instance.cherries = 0;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
