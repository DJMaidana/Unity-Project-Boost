using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rocketRb;
    public AudioSource audioSource;
    [SerializeField] AudioSource directionalBoosters;

    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotationSpeed = 100;
    [SerializeField] AudioClip rocketBoostSound;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;

    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketThrust();
        RocketRotation();
    }

    void RocketThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mainBooster.Play();
            rocketRb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if(!audioSource.isPlaying)
            {    
                audioSource.PlayOneShot(rocketBoostSound);
            }
        }
        else if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void RocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RightBooster.Play();
            ApplyRotation(rotationSpeed);

            if (!directionalBoosters.isPlaying)
            {
                directionalBoosters.Play();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            LeftBooster.Play();
            ApplyRotation(-rotationSpeed);

            if (!directionalBoosters.isPlaying)
            {
                directionalBoosters.Play();
            }
        }
        else
        {
            directionalBoosters.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRb.freezeRotation = false;
    }
}
