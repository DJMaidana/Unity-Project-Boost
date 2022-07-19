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
                Debug.Log("Hit Friendly");
                break;
            
            case "Obstacle":
                Debug.Log("Hit Obstacle");
                break;
            
            case "Start":
                Debug.Log("Hit Start");
                break;

            case "Finish":
                Debug.Log("Hit Finish");
                break;
            
            default:
                break;
        }    
    }
}
