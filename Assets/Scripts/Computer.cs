using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public Finish finish;

    [SerializeField]
    private AudioSource computerSound;

    [SerializeField]
    private Text computerText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            computerText.text = "Press X to turn off the gate.";
        }
    }

    public void Unlock()
    {
        computerSound.Play();
        Destroy(GameObject.FindGameObjectWithTag("Gate"));
        Destroy(GameObject.FindGameObjectWithTag("Block"));
        computerText.text = "Gate has been unlocked.";
        finish.levelComplete = true;
    }

    private void OnCollisionExit2D(Collision2D collison)
    {
        computerText.text = "";
    }
}
