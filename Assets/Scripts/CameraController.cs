using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform player; 

    [SerializeField]
    private AudioReverbFilter audioReverb;

    public bool reverbEnabled = false;

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    public void ToggleReverb()
    {
        audioReverb.enabled = !reverbEnabled;
    }
}
