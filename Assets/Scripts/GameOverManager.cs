using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip[] fallingSounds;
    public AudioClip[] gameOverSounds;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
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
    void Update()
    {
        int gameOverElement = Random.Range(0, gameOverSounds.Length);
        int fallingElement = Random.Range(0, fallingSounds.Length);

        if (fallingElement == 2)
        {
            StopAllAudio();
            playerAudio.PlayOneShot(fallingSounds[fallingElement], 0.5f);
        }

        else
        {
            StopAllAudio();
            playerAudio.PlayOneShot(fallingSounds[fallingElement], 1.0f);
        }

        playerAudio.PlayOneShot(gameOverSounds[gameOverElement], 0.7f);
    }
}
