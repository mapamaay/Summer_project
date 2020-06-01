using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Controller variables//
    PlayerControls controls;
    public CharacterController controller;

    //Mouvement variables//
    Vector2 move;
    public float moveSpeed = 3;

    //Physics variables//
    Vector3 fallVelocity;
    public float gravity = -9.81f;

    //Smoothing variables//
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;
    public float joystickSensibility = 0.1f;

    private void Awake()
    {
        //Controller setup//
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    private void Update()
    {
        //Get joystick direction//
        Vector3 direction = new Vector3(-move.x, 0f, -move.y);
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
        if (move.magnitude >= joystickSensibility) 
        {
            //Turn//
            float lookAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            //Move//
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }

    }

    //Disable controller if player object is disabled//
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
