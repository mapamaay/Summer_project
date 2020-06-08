using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Controller variables//
    public CharacterController controller;

    //Mouvement variables//
    Vector2 stickInput;
    public float moveSpeed = 3;

    //Physics variables//
    Vector3 fallVelocity;
    public float gravity = -9.81f;

    //Smoothing variables//
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;
    public float joystickDeadzone = 0.1f;

    private void OnMove(InputValue LS)
    {
        //Get joystick direction//
        stickInput = LS.Get<Vector2>();
    }
    private void Update()
    {
        //Set direction//
        Vector3 direction = new Vector3(-stickInput.x, 0f, -stickInput.y);
        direction = Mathf.Clamp(direction.magnitude, 0f, 1f) * direction.normalized;

        //Gravity//
        //Gravity reset//
        if (controller.isGrounded && fallVelocity.y <=-2)
        {
            fallVelocity.y = -2f;
        }

         //Gravity force over time//
        if(!controller.isGrounded)
        {
            fallVelocity.y += gravity * Time.deltaTime;
            controller.Move(fallVelocity * Time.deltaTime);
        }

        //Move if joystick is pushed far enough//
        if (stickInput.magnitude<joystickDeadzone)
        {
            stickInput = Vector2.zero;
        }

        else
        {
            //Turn//
            float lookAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            //Move//
            controller.Move(direction * moveSpeed * Time.deltaTime * ((stickInput.magnitude - joystickDeadzone) / (1 - joystickDeadzone)));
        }

    }

}