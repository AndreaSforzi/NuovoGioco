using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerControls;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 1;

    float gravity = -30f;
    
    Vector2 horizontalMovement;
    float verticalMovement;

    private void Awake()
    {
        playerControls = new PlayerInput();
    }

    


    private void Update()
    {
        HandleMovement();

    }

    private void HandleMovement()
    {
        playerControls.Navigation.Movement.performed += Movement_performed;
        playerControls.Navigation.Movement.canceled += Movement_canceled;


        verticalMovement += gravity * Time.deltaTime;

        controller.Move(new Vector3(horizontalMovement.x, verticalMovement, horizontalMovement.y) * Time.deltaTime * speed);

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
