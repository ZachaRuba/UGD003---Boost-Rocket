using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You bumped into a friendly!");
                break;
            case "Finish":
                Debug.Log("You won the game!");
                break;
            default:
                Debug.Log("Oops! You bumped into an obstacle.");
                //SceneManager.LoadScene("SampleScene");
                ReloadLevel();
                break;
        }
            
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
