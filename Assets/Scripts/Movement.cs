using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int thrust = 1000;
    [SerializeField] private float rotationSpeed = 10;

    Rigidbody myRigidBody;
    GameObject launchPad;
    Vector3 startPosition;
    AudioSource audioSource;
    private bool isPlayingAudio = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
        try
        {
            launchPad = GameObject.Find("Launch Pad");
        }
        catch{
            Debug.Log("No Launch Pad in Scene");
        }

    }

    // Update is called once per frame
    void Update()
    {
        ProcessReset();
        ProcessThrust();
        ProcessRotation();
    }


    private void ProcessReset()
    {
        if (Input.GetKey(KeyCode.R)) 
        {
            Debug.Log("Reset...");
            myRigidBody.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.position = startPosition;
        }
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            Debug.Log("Thrusting...");
            myRigidBody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if(!isPlayingAudio)
            {
                isPlayingAudio = true;
                audioSource.Play();
            }
        }
        else
        {
            if (isPlayingAudio)
            {
                isPlayingAudio = false;
                audioSource.Stop();
            }
        }

    }

    void ProcessRotation()
    {
        float angle = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating CCW...");
            angle -= rotationSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating CW...");
            angle += rotationSpeed * Time.deltaTime;
        }
        
        if(angle != 0f)
        {
            myRigidBody.freezeRotation = true;
            transform.Rotate(Vector3.forward, angle);
            myRigidBody.freezeRotation = false;
        }
    }
}
