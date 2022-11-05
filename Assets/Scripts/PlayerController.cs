using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody playerRb;

    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Camera");
    }

    // on entering the trigger of ___
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("x"))
        //{

        //}
    }

    // on collision with x
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("x"))
        //{

        //}
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
