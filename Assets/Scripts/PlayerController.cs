using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody playerRb;
    public GameObject Camera;

    private AudioSource playerAudio;
    public AudioClip[] wallHitSounds;
    public AudioClip[] victorySounds;
    public AudioClip[] fallingSounds;
    public AudioClip[] gameOverSounds;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Camera");
        playerAudio = GetComponent<AudioSource>();
    }

    // on entering the trigger of ___
    private void OnTriggerEnter(Collider other)
    {
        int gameOverElement = Random.Range(0, gameOverSounds.Length);
        int victoryElement = Random.Range(0, victorySounds.Length);
        int fallingElement = Random.Range(0, fallingSounds.Length);

        if (other.CompareTag("VictoryBox"))
        {
            StopAllAudio();
            playerAudio.PlayOneShot(victorySounds[victoryElement], 1f);
        }

        if (other.CompareTag("FallingThrough"))
        {
            StopAllAudio();
            playerAudio.PlayOneShot(fallingSounds[fallingElement], 1.0f);
        }

        if (other.CompareTag("Dead"))
        {
            playerAudio.PlayOneShot(gameOverSounds[gameOverElement], 1.0f);
        }
    }

    // on collision with x
    private void OnCollisionEnter(Collision collision)
    {
        int hitElement = Random.Range(1, wallHitSounds.Length);
        if (collision.gameObject.CompareTag("MazeWall"))
        {
            if (hitElement == 1 || hitElement == 9)
            {
                playerAudio.PlayOneShot(wallHitSounds[hitElement], 0.4f);
            }
            else if (hitElement == 11)
            {
                playerAudio.PlayOneShot(wallHitSounds[hitElement], 0.1f);
            }

            else
            {
                playerAudio.PlayOneShot(wallHitSounds[hitElement], 1.0f);
            }
            Debug.Log("wall collision");
        }

        if (collision.gameObject.CompareTag("OuterWall"))
        {
            playerAudio.PlayOneShot(wallHitSounds[0], 0.2f);
            Debug.Log("outerwall collision");
        }
    }

    private AudioSource[] allAudioSources;

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // player moves forwards/backwards based on W/S input and the camera's axis
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Camera.transform.forward * speed * forwardInput);
        playerRb.AddForce(Camera.transform.right * speed * horizontalInput);

    }
}
