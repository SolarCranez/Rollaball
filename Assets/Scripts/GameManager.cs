using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public KeyCode restart { get; set; }
    public bool gameOver = false;

    private void Awake()
    {
        Instance = this;

        restart = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("restartKey", "R"));
    }

    // Start is called before the first frame update
    void Start()
    {
        int backrooms = Random.Range(0, 10);
        if (backrooms==2)
        {
            SceneManager.LoadScene(2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(GameManager.Instance.restart))
        {
            SceneManager.LoadScene(0);
        }
    }
}
