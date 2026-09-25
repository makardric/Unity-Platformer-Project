using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerJump : MonoBehaviour
{
    
    [SerializeField] private float jumpForce = 100f;
    private Rigidbody2D rigidBody;
    private Animator animator;
    //[SerializeField] private float tapJumpForce = 3f;
    //[SerializeField] private float holdJumpForce = 10f;


    private bool onGround = true;
    private bool isJumping = false;
    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        // jumping range ( > 0)
        // mid air range (-0.5 - 0.5)
        // falling range ( < 0)
        if (isJumping && (rigidBody.linearVelocityY > 0f))
        {
            Debug.Log("Jump");
            animator.SetBool("onGround", false);
            animator.SetBool("isJumping", true);
        }
        else if (isJumping && (rigidBody.linearVelocityY > -0.5f) && (rigidBody.linearVelocityY < 0.5f))
        {
            Debug.Log("Mid Air");
            animator.SetBool("isJumping", false);
            animator.SetBool("isMidAir", true);
        }
        else if (isJumping &&  rigidBody.linearVelocityY < 0f)
        {
            Debug.Log("Falling");
            animator.SetBool("isMidAir", false);
            animator.SetBool("isFalling", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Ground being touched");
            animator.SetBool("isJumping", false);
            animator.SetBool("isMidAir", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("onGround", true);
            isJumping = false;
        }
    }
    private void OnJump(InputValue value)
    {
        //Debug.Log("am i mutted chat");
        // if on ground
        // different amounts of time W is held = different force exerted on character
        if (value.isPressed)
        {
            if (onGround && !isJumping)
            {
                rigidBody.AddForce(Vector2.up * jumpForce);
                isJumping = true;
            }
        }
    }
}
