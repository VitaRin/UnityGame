using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField]
    private Text cherries;

    void Start()
    {
        cherries.text = "Cherries: " + GlobalControl.Instance.cherries;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
