using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public float speed = 6f;
    float turnSmoothVelocity;

    public CapsuleCollider collider;



    private float turnSpeed = 150f;

    [SerializeField] float gravity;

    private bool _jumpPressed = false;

    private Vector2 inputVector;

    private Vector3 velocity;

    private Vector3 direction;

    [SerializeField] float _jumpHeight = 50f;

    void Start() {
        collider = GetComponent<CapsuleCollider>();
        // InputSystem playeractions = new InputSystem();
        // playeractions.Player.Enable();
        // playeractions.Player.Jump.performed += jump();
    }
    // Update is called once per frame
    void Update()
    {
        float y_value = direction.y + gravity*Time.deltaTime;
        direction = new Vector3(inputVector.x, y_value, inputVector.y);
        if (_jumpPressed && controller.isGrounded)
        {
            direction.y += Mathf.Sqrt(_jumpHeight * -1.0f* gravity);
            _jumpPressed = false;
        }

        // transform.Rotate(Vector3.up, inputVector.x*turnSpeed *Time.deltaTime);
        
        controller.Move(direction*speed*Time.deltaTime);
        
        MovementJump();
        
    }

    void MovementJump()
    {
        if (controller.isGrounded)
        {
            velocity.y = 0.0f;
        }
    }
    public void jump()
    {
        if (controller.velocity.y == 0)
        {
            _jumpPressed = true;
        }
    }
    public void OnMove(InputAction.CallbackContext context){
        inputVector = context.ReadValue<Vector2>();
        // if (context.control == InputAction.InputControl.a)
        // {
        //     print("here");
        // }
        
            
    }
    
}
