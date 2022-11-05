using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float tiltSpeed;
    private float forwardInput;
    private float horizontalInput;

    private Vector3 currentEulerAngles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // return a Vector3 for rotation
    public Vector3 Rotation(float x, float z)
    {
        return (new Vector3(x, 0, -z) * tiltSpeed * Time.deltaTime);
    }

    
    // rotate the platform/object
    public void RotatePlatform()
    {
        //modifying the Vector3, based on input multiplied by speed and time
        currentEulerAngles += (Rotation(forwardInput, horizontalInput));
        //apply the change to the gameObject
        transform.eulerAngles = currentEulerAngles;
    }


    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        RotatePlatform();
    }
}
