using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    //public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = new Vector3(0, 11.72f, -5.03f) - player.transform.position;
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        //if (Input.GetKeyDown("Q") || Input.GetKeyDown("E"))
        //{
        //    Debug.Log("Q or E was pressed");

        //}

        //transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
        transform.position = player.transform.position + offset;
    }

}
