using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackroomsController : MonoBehaviour
{
    // variables
    public float speed;
    public Rigidbody playerRb;
    public GameObject Camera;

    // audio variables
    private AudioSource playerAudio;
    public AudioClip[] wallHitSounds;
    public AudioClip[] victorySounds;
    //public AudioClip[] fallingSounds;
    public AudioClip[] gameOverSounds;
    public AudioClip[] horrorSounds;
    public AudioClip[] horrorSounds2;
    //private bool dead = false;

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

        // victory: stop all movement and audio, play victory sound
        if (other.CompareTag("VictoryBox") && BackroomsGameManager.Instance.gameOver==false)
        {
            BackroomsGameManager.Instance.gameOver = true;
            StopAllAudio();
            while (playerAudio.isPlaying == false)
            {
                playerAudio.PlayOneShot(victorySounds[victoryElement], 1f);
            }
            SceneManager.LoadScene(0);
        }

        // fell through hole: stop all movement and audio, play gameOver sound
        if (other.CompareTag("FallingThrough") && BackroomsGameManager.Instance.gameOver == false)
        {
            BackroomsGameManager.Instance.gameOver = true;

            StopAllAudio();
            playerAudio.PlayOneShot(gameOverSounds[gameOverElement], 0.5f);
        }
    }

    // on collision with "x" variable
    private void OnCollisionEnter(Collision collision)
    {
        // random range from 1 to array length
        int hitElement = Random.Range(1, 100);
        // on collision with an inner maze wall, play hit sound
        if ((collision.gameObject.CompareTag("MazeWall") || collision.gameObject.CompareTag("OuterWall")) && (hitElement == 1))
        {
            playerAudio.PlayOneShot(wallHitSounds[0], 0.4f);
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

    bool horrorActive = false;
    IEnumerator Horror()
    {
        horrorActive = true;

        if (BackroomsGameManager.Instance.gameOver == true)
        {
            StopAllCoroutines();
        }

        int delay = Random.Range(5, 10);
        Debug.Log("initial delay is.." + delay);
        yield return new WaitForSeconds(delay);

        delay = Random.Range(10, 20);
        Debug.Log("new delay time is.." + delay);
        yield return new WaitForSeconds(delay);

        int horrorElement = Random.Range(0, horrorSounds.Length);
        playerAudio.PlayOneShot(horrorSounds[horrorElement], 0.7f);
        yield return new WaitUntil(() => playerAudio.isPlaying == false);

        horrorActive = false;
        StopAllCoroutines();
    }

    IEnumerator Horror2()
    {
        if (BackroomsGameManager.Instance.gameOver == true)
        {
            StopAllCoroutines();
        }

        int delay = Random.Range(20, 30);
        yield return new WaitForSeconds(delay);


        int horrorElement2 = Random.Range(0, horrorSounds2.Length);
        playerAudio.PlayOneShot(horrorSounds2[horrorElement2], Random.Range(0, 1));
        yield return new WaitUntil(() => playerAudio.isPlaying == false);

        horrorActive = false;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (horrorActive==false) {
            StartCoroutine(Horror());
        }

        //while (horrorActive==true)
        //{
        //    StartCoroutine(Horror2());
        //}

        if (BackroomsGameManager.Instance.gameOver == true)
        {
            StopAllCoroutines();
        }
    }
}
