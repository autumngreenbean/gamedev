using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrid_person_movement : MonoBehaviour
{
    public CharacterController controller;


    public float speed = 6f;

    
    
    // time to turn 90 degrees
    public float turnSmoothTime = .1f;

    //private variable to store the angle
    float turnSmoothVelocity; 




    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= .1f){
            // compute angle between the x and y axis
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // turn at a given rate "smooths" the angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // transform to angle every update
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // move player at a linear speed 
            controller.Move(direction * speed * Time.deltaTime);

        }

    }
}
