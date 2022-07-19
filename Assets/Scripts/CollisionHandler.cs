using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float sceneLoadDelay = 1;

    private void OnCollisionEnter(Collision other)
    { 
        switch (other.gameObject.tag)
        {           
            case "Start":
                Debug.Log("Hit Start");
                break;

            case "Finish":
                Debug.Log("Hit Finish");
                SuccessSequence();
                break;

            default:
                CrashSequence();
                break;
        }    
    }

    void SuccessSequence()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerController>().audioSource.Stop();
        Invoke("LoadNextLevel", sceneLoadDelay);
    }

    void CrashSequence()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerController>().audioSource.Stop();
        Invoke("ReloadLevel", sceneLoadDelay);
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
