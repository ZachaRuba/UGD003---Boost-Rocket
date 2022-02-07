using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int thrust = 1000;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem thrusterCWParticles;
    [SerializeField] private ParticleSystem thrusterCCWParticles;


    Rigidbody myRigidBody;
    AudioSource audioSource;
    
    private bool isMainEngineThrusting = false;
    private bool isCCWEngineThrusting = false;
    private bool isCWEngineThrusting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        bool mainEngineThrustButtonPressed = Input.GetKey(KeyCode.Space);

        if (mainEngineThrustButtonPressed && !isMainEngineThrusting)
            TurnOnMainEngine();
        else if (!mainEngineThrustButtonPressed && isMainEngineThrusting)
            TurnOffMainEngine();

        if (isMainEngineThrusting)
            UpdateMainEngineThrust();

    }

    void ProcessRotation()
    {
        bool isCCWEngineThrustButtonPressed = Input.GetKey(KeyCode.A);
        bool isCWEngineThrustButtonPressed = Input.GetKey(KeyCode.D);

        if (isCCWEngineThrustButtonPressed && !isCCWEngineThrusting) TurnOnCCWEngine();
        else if (!isCCWEngineThrustButtonPressed && isCCWEngineThrusting) TurnOffCCWThruster();

        if (isCWEngineThrustButtonPressed && !isCWEngineThrusting) TurnOnCWEngine();
        else if (!isCWEngineThrustButtonPressed && isCWEngineThrusting) TurnOffCWThruster();

        if (isCCWEngineThrusting || isCWEngineThrusting)
            UpdateRotationThrust();
    }

    private void TurnOnMainEngine() 
    {
        isMainEngineThrusting = true;
        audioSource.PlayOneShot(mainEngine);
        mainEngineParticles.Play();
    }

    private void TurnOffMainEngine()
    {
        isMainEngineThrusting = false;
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void UpdateMainEngineThrust()
    {
        Debug.Log("Thrusting...");
        myRigidBody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
    }

    private void TurnOnCCWEngine() 
    {
        thrusterCCWParticles.Play();
        isCCWEngineThrusting = true;
    }

    private void TurnOffCCWThruster()
    {
        thrusterCCWParticles.Stop();
        isCCWEngineThrusting = false;
    }

    private void TurnOnCWEngine() 
    {
        thrusterCWParticles.Play();
        isCWEngineThrusting = true;
    }

    private void TurnOffCWThruster()
    {
        thrusterCWParticles.Stop();
        isCWEngineThrusting = false;
    }
    private void UpdateRotationThrust()
    {
        float rotationAngle;

        if (!isCCWEngineThrusting && isCWEngineThrusting) rotationAngle = rotationSpeed * Time.deltaTime;
        else if (isCCWEngineThrusting && !isCWEngineThrusting) rotationAngle = -rotationSpeed * Time.deltaTime;
        else return;

        myRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward, rotationAngle);
        myRigidBody.freezeRotation = false;
    }
}
