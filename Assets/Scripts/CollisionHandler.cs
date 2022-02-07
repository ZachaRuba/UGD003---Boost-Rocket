using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                break;
        }
            
    }
}
