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
                Debug.Log("Hit Friendly");
                break;
            
            case "Start":
                Debug.Log("Hit Start");
                break;

            case "Finish":
                Debug.Log("Hit Finish");
                LoadNextLevel();
                break;
            
            default:
                ReloadLevel();
                break;
        }    
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(activeSceneIndex);
    }

    void LoadNextLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene;

        if (activeSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            nextScene = activeSceneIndex + 1;
        }
        else
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
}
