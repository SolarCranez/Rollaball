using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player reference and camera offset variables
    public GameObject player;
    private Vector3 offset;

    // camera audio
    public AudioSource cameraAudio;

    // Start is called before the first frame update
    void Start()
    {
        // get components/gameobjects
        cameraAudio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        offset = new Vector3(0, 11.72f, -5.03f) - player.transform.position;
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // move camera based on player and offset
        transform.position = player.transform.position + offset;
    }

}
