using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerControls;
    [SerializeField] Camera playerCamera;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 1; 

    float gravity = -30f;

    Vector2 lookMovement;
    Vector2 horizontalMovement;

    float xRotation = 0;
    float verticalMovement;




    private void Awake()
    {
        playerControls = new PlayerInput();
    }

    


    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    private void HandleLook()
    {
        playerControls.Navigation.MouseLook.performed += MouseLook_performed;

        xRotation -= (lookMovement.y * Time.deltaTime) * 10f;
        xRotation = Mathf.Clamp(xRotation, -80f, 80);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookMovement.x * Time.deltaTime) * 10f);
    }

    private void MouseLook_performed(InputAction.CallbackContext obj)
    {
        lookMovement = playerControls.Navigation.MouseLook.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        playerControls.Navigation.Movement.performed += Movement_performed;
        playerControls.Navigation.Movement.canceled += Movement_canceled;

        
        verticalMovement += gravity * Time.deltaTime;

        Vector3 moveDirection = new Vector3(horizontalMovement.x, verticalMovement, horizontalMovement.y);

        controller.Move(transform.TransformDirection(moveDirection) * Time.deltaTime * speed);
        
    }

  

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        horizontalMovement = Vector2.zero;
    }

    private void Movement_performed(InputAction.CallbackContext obj)
    {
        horizontalMovement = playerControls.Navigation.Movement.ReadValue<Vector2>();
    }

    

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

}
