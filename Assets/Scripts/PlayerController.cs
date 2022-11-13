using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // variables
    public float speed;
    public Rigidbody playerRb;
    public GameObject Camera;

    // audio variables
    private AudioSource playerAudio;
    public AudioClip[] wallHitSounds;
    public AudioClip[] victorySounds;
    public AudioClip[] fallingSounds;
    public AudioClip[] gameOverSounds;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        // get components/gameobjects
        playerRb = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Camera");
        playerAudio = GetComponent<AudioSource>();
    }

    // on entering the trigger of ___
    private void OnTriggerEnter(Collider other)
    {
        // random range from 0 to length of array
        int gameOverElement = Random.Range(0, gameOverSounds.Length);
        int victoryElement = Random.Range(0, victorySounds.Length);
        int fallingElement = Random.Range(0, fallingSounds.Length);

        // victory: stop all movement and audio, play victory sound
        if (other.CompareTag("VictoryBox") && GameManager.Instance.gameOver==false)
        {
            GameManager.Instance.gameOver = true;
            StopAllAudio();
            playerAudio.PlayOneShot(victorySounds[victoryElement], 1f);
        }

        // fell through hole: stop all movement and audio, play falling sound
        if (other.CompareTag("FallingThrough") && GameManager.Instance.gameOver==false)
        {
            GameManager.Instance.gameOver = true;

            //if (fallingElement==2)
            //{
            //    StopAllAudio();
            //    playerAudio.PlayOneShot(fallingSounds[fallingElement], 0.5f);
            //}
            //else
            //{
            //    StopAllAudio();
            //    playerAudio.PlayOneShot(fallingSounds[fallingElement], 1.0f);
            //}
        }

        // hit bottom of maze: play death sound
        if (other.CompareTag("Dead") && !dead)
        {
            dead = true;
            //playerAudio.PlayOneShot(gameOverSounds[gameOverElement], 0.7f);
            SceneManager.LoadScene(1);
        }
    }

    // on collision with "x" variable
    private void OnCollisionEnter(Collision collision)
    {
        // random range from 1 to array length
        int hitElement = Random.Range(1, wallHitSounds.Length);
        // on collision with an inner maze wall, play hit sound
        if (collision.gameObject.CompareTag("MazeWall"))
        {
            if (hitElement == 1 || hitElement == 9 || hitElement == 12)
            {
                playerAudio.PlayOneShot(wallHitSounds[hitElement], 0.4f);
            }

            else
            {
                playerAudio.PlayOneShot(wallHitSounds[hitElement], 1.0f);
            }
            //Debug.Log("wall collision");
        }

        // on collision with outer maze wall, play pipe sfx
        if (collision.gameObject.CompareTag("OuterWall") && GameManager.Instance.gameOver == false)
        {
            playerAudio.PlayOneShot(wallHitSounds[0], 0.2f);
            //Debug.Log("outerwall collision");
        }
    }

    // AudioSource array specifically for StopAllAudio
    private AudioSource[] allAudioSources;
    // stops all audio being played
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

    }
}
