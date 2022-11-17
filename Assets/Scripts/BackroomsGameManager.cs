using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackroomsGameManager : MonoBehaviour
{
    public static BackroomsGameManager Instance { get; private set; }

    public KeyCode restart { get; set; }
    public bool gameOverB = false;
    public bool win = false;

    private void Awake()
    {
        Instance = this;

        restart = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("restartKey", "R"));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(BackroomsGameManager.Instance.restart))
        {
            SceneManager.LoadScene(0);
        }
    }
}
