using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float sceneLoadDelay = 1;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem VFX_Success;
    [SerializeField] ParticleSystem VFX_Crash;
    [SerializeField] ParticleSystem VFX_Smoke;

    AudioSource audioSource;
    PlayerController playerController;

    bool isTransitioning = false;
    [SerializeField] bool collisionsEnabled = true;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsEnabled = !collisionsEnabled;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
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
                    if (collisionsEnabled)
                    {
                        CrashSequence();
                    }
                    break;
            }
        }    
    }

    void SuccessSequence()
    {
        VFX_Success.Play();
        isTransitioning = true;
        playerController.directionalBoosters.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        playerController.enabled = false;
        Invoke("LoadNextLevel", sceneLoadDelay);
    }

    void CrashSequence()
    {
        VFX_Crash.Play();
        VFX_Smoke.Play();
        isTransitioning = true;
        playerController.directionalBoosters.Stop();
        audioSource.Stop();
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(crash);
        playerController.enabled = false;
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
