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
    
    public bool hasKey = false;

    public bool keyPressed = false;

    private Finish finish;

    public Computer computer;

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

        //  if (collision.gameObject.CompareTag("Computer"))
        //  {
        //      computerText.text = "Press X to turn off the gate.";

        //      if (Input.GetKeyDown(KeyCode.X))
        //      {
        //          computerSound.Play();
        //          Destroy(GameObject.FindGameObjectWithTag("Gate"));
        //          computerText.text = "Gate has been unlocked.";
        //          finish.levelComplete = true;
        //      }
        //  }
        
    }

    //  private void OnCollisionExit2D(Collision2D collision)
    //  {
    //      if (collision.gameObject.CompareTag("Computer"))
    //      {
    //          computerText.text = "";
    //     }
    // }

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
