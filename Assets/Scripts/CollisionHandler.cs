using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float sceneLoadDelay = 1;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    { 
        if (!isTransitioning)
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
    }

    void SuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<PlayerController>().enabled = false;
        Invoke("LoadNextLevel", sceneLoadDelay);
    }

    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(crash);
        GetComponent<PlayerController>().enabled = false;
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
