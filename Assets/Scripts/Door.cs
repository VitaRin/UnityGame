using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{   
    private Animator anime;

    [SerializeField]
    private Text doorText;

    [SerializeField]
    private AudioSource doorSound;

    public PlayerCollide playerCollide;

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && playerCollide.hasKey == true)
        {
            doorSound.Play();
            anime.SetBool("HasKey", true);
        }
        else if (collision.gameObject.name == "Player" && playerCollide.hasKey == false)
        {
            doorText.text = "You need a key to open this door.";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            doorText.text = "";
        }
    }

    private void DestroyDoor()
    {
        Destroy(this.gameObject);
    }
}
