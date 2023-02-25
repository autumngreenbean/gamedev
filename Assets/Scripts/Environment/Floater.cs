using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rb;

    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;

    public float waterDrag = 1.0f;
    public float waterAngularDrag = 0.5f;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(
            Physics.gravity / floaterCount,
            transform.position,
            ForceMode.Acceleration
        );

        float waveHeight = WaveManager.Instance.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier =
                Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged)
                * displacementAmount;

            rb.AddForceAtPosition(
                new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),
                transform.position,
                ForceMode.Acceleration
            );

            rb.AddForce(
                displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime,
                ForceMode.VelocityChange
            );

            rb.AddTorque(
                displacementMultiplier
                    * -rb.angularVelocity
                    * waterAngularDrag
                    * Time.fixedDeltaTime,
                ForceMode.VelocityChange
            );
        }
    }
}
