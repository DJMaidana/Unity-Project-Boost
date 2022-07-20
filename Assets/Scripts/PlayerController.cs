using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rocketRb;
    public AudioSource audioSource;             // Public so CollisionHandler can stop them on Events
    public AudioSource directionalBoosters;     //

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
            StartThrusting();
        }
        else if (audioSource.isPlaying)
        {
            StopThrusting();
        }
    }

    void RocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateCounterclockwise();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateClockwise();
        }
        else
        {
            directionalBoosters.Stop();     //Stops audio when directional keys are released
        }
    }

    void StartThrusting()
    {
        mainBooster.Play();
        rocketRb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketBoostSound);
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
    }

    void RotateCounterclockwise()
    {
        RightBooster.Play();
        ApplyRotation(rotationSpeed);

        if (!directionalBoosters.isPlaying)
        {
            directionalBoosters.Play();
        }
    }

    private void RotateClockwise()
    {
        LeftBooster.Play();
        ApplyRotation(-rotationSpeed);

        if (!directionalBoosters.isPlaying)
        {
            directionalBoosters.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRb.freezeRotation = false;
    }
}
