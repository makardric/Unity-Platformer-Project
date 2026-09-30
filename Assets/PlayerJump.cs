using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerJump : MonoBehaviour
{
    
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private PlayerMovement playerMovement;

    private Rigidbody2D rigidBody;
    private Animator animator;
    //[SerializeField] private float tapJumpForce = 3f;
    //[SerializeField] private float holdJumpForce = 10f;

    //0 = Idle
    //1 = Run
    //2 = Jump
    //3 = MidAir
    //4 = Fall


    private bool onGround = true;
    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        // jumping range ( > 0)
        // mid air range (-0.5 - 0.5)
        // falling range ( < 0)
        int state;

        if (onGround)
        {
            //Debug.Log("Ground being touched");
            if (playerMovement.IsMoving)
                // run
                state = 1; 
            else
            {
                // idle
                state = 0;
            }
        }

        else if (rigidBody.linearVelocityY > 0.5f)
        {
            //Debug.Log("Jump");
            state = 2;
        }
        else if (rigidBody.linearVelocityY < -0.1f)
        {
            //Debug.Log("Falling");
            state = 4;
        }
        else
        {
            //Debug.Log("Mid-Air");
            state = 3;
        }

    Debug.Log($"onGround={onGround}, velY={rigidBody.linearVelocityY}, state={state}");
    animator.SetInteger("AnimationState", state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        { 
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            onGround = false;
    }

    private void OnJump(InputValue value)
    {
        //Debug.Log("am i mutted chat");
        // if on ground
        // different amounts of time W is held = different force exerted on character
        if (value.isPressed && onGround)
        {
            rigidBody.AddForce(Vector2.up * jumpForce);
            onGround = false;
        }
    }
}
