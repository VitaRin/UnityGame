using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{  
    private int cherries = 0;
    
    [SerializeField]
    private Text pointsText;

    [SerializeField]
    private AudioSource cherrySound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            cherrySound.Play();
            Destroy(collision.gameObject);
            cherries++;
            pointsText.text = "Cherries: " + cherries;
        }
    }
}
 