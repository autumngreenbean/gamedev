using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float forwardSpeed = 1f;

    [SerializeField]
    private float backSpeed = 1f;

    [SerializeField]
    private float rotateSpeed = 1f;

    //////////////////////////////////////////////////
    // Private Fields and Methods //
    //////////////////////////////////////////////////

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 localVelocity = transform.InverseTransformDirection(rigidBody.velocity);

        if (localVelocity.z > 0f)
        {
            rigidBody.AddRelativeTorque(new Vector3(0f, horizontalInput * rotateSpeed, 0f));
        }
        else if (localVelocity.z < 0f)
        {
            rigidBody.AddRelativeTorque(new Vector3(0f, -horizontalInput * rotateSpeed, 0f));
        }

        if (verticalInput > 0)
            rigidBody.AddForce(transform.forward * verticalInput * forwardSpeed);
        else
            rigidBody.AddForce(transform.forward * verticalInput * backSpeed);
    }
}
