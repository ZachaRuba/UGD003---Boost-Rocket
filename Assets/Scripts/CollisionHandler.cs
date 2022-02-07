using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 5f;
    [SerializeField] private AudioClip crash;
    [SerializeField] private AudioClip land;

    [SerializeField] private ParticleSystem crashParticles;
    [SerializeField] private ParticleSystem landParticles;

    [SerializeField] bool debugMode = false;

    AudioSource collisionSource;

    private bool isTransitioning = false;
    private bool debugTurnCollisionOff = false;

    private void Update()
    {
        if (debugMode) 
        {
            UpdateDebugKeys();
        }   
    }

    void UpdateDebugKeys() 
    {
        if (Input.GetKeyDown(KeyCode.L)) LoadNextLevel();
        if (Input.GetKeyDown(KeyCode.C)) debugTurnCollisionOff = !debugTurnCollisionOff;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (!debugTurnCollisionOff) {
            collisionSource = GetComponent<AudioSource>();

            if (!isTransitioning)
            {
                switch (other.gameObject.tag)
                {
                    case "Friendly":
                        Debug.Log("You bumped into a friendly!");
                        break;
                    case "Finish":
                        Debug.Log("You won the game!");
                        StartLandingSequence();
                        break;
                    default:
                        Debug.Log("Oops! You bumped into an obstacle.");
                        //SceneManager.LoadScene("SampleScene");
                        StartCrashSequence();
                        break;
                }
            }
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        collisionSource.Stop();
        collisionSource.PlayOneShot(crash);
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);

    }

    void StartLandingSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        collisionSource.Stop();
        collisionSource.PlayOneShot(land);
        landParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(NextSceneIndex);
    }
}
