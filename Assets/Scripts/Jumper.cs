using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Useless script example.
/// </summary>
public class Jumper : MonoBehaviour
{
    public float jumpSpeed = 1f;
    public float jumpHeight = 1f;
    public float jumpStartOffset = 1f;
    public float timingOffset = 1f;

    private bool jumping = false;
    private bool reallyJumping = false;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
        StartCoroutine(StartJump());
    }

    public float ratio = 0f;

    private void Update()
    {
        ratio = Mathf.PingPong((Time.time + timingOffset) * jumpSpeed, 1f);
        if (jumping && ratio < 0.01f)
            reallyJumping = true;

        if (reallyJumping)
            transform.position = new Vector3(
                transform.position.x,
                startY + jumpHeight * ratio,
                transform.position.z
            );
    }

    private IEnumerator StartJump()
    {
        yield return new WaitForSeconds(jumpStartOffset);
        jumping = true;
    }
}
