using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{   
    private Animator anime;

    [SerializeField]
    private Text doorText;

    public PlayerCollide playercollide;

    void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && playercollide.hasKey == true)
        {
            anime.SetBool("HasKey", true);
        }
        else if (playercollide.hasKey == false)
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
