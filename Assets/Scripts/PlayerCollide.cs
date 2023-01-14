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
    private Text computerText;

    [SerializeField]
    private AudioSource cherrySound;  

    [SerializeField]
    private AudioSource keySound; 

    [SerializeField]
    private AudioSource computerSound; 

    [SerializeField]
    private AudioSource zappedSound; 
    
    public bool hasKey = false;

    public bool keyPressed = false;

    public Computer computer;

    public CameraController cameraController;

    public Knockback knockback;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        pointsText.text = "Cherries: " + GlobalControl.Instance.cherries;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            GlobalControl.Instance.HP--;

            // anime.SetTrigger("Hit");

            // Vector2 direction = new Vector2(player.transform.position.x - collision.gameObject.transform.position.x, 1.3f);
            // knockback.Feedback(direction, 7f);
            // anime.SetInteger("State", 0);
            Die();
        }

        if (collision.gameObject.CompareTag("Gate"))
        {
            zappedSound.Play();
            //anime.SetTrigger("Hit");
            Vector2 direction = new Vector2(player.transform.position.x - collision.gameObject.transform.position.x, 0.7f);
            knockback.Feedback(direction, 8f);
            //anime.SetInteger("State", 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Computer"))
        {
            if (Input.GetKey(KeyCode.X))
            {
                computer.Unlock();
            }
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
            keySound.Play();
            Destroy(collision.gameObject);
            hasKey = true;
        }
        if (collision.gameObject.CompareTag("Freefall"))
        {
            player.mass = 5f;
            player.gravityScale = 3.72f;
            cameraController.ToggleReverb();
            Invoke("Die", 3f);
        }
    }

    // private IEnumerator Knockback(float knockbackDuration, float knockbackPower, Vector3 direction) {
    //     float timer = 0;

    //     while (knockbackDuration > timer)
    //     {
    //         timer += Time.deltaTime;
    //         player.AddForce(direction);
    //     }

    //     yield return 0;
    // }

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
