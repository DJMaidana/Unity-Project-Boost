using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rocketRb;

    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotationSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
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
            rocketRb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void RocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRb.freezeRotation = false;
    }
}