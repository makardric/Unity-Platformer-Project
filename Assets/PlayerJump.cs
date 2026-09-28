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
        if (onGround)
        {
            //Debug.Log("Ground being touched");
            animator.SetBool("isJumping", false);
            animator.SetBool("isMidAir", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("onGround", true);
            return;
        }

        animator.SetBool("onGround", false);

        if (rigidBody.linearVelocityY > 0.5f)
        {
            //Debug.Log("Jump");
            animator.SetBool("isJumping", true);
            animator.SetBool("isMidAir", false);
            animator.SetBool("isFalling", false);
        }
        else if (rigidBody.linearVelocityY < -0.1f)
        {
            //Debug.Log("Falling");
            animator.SetBool("isJumping", false);
            animator.SetBool("isMidAir", false);
            animator.SetBool("isFalling", true);
        }
        else
        {
            //Debug.Log("Mid-Air");
            animator.SetBool("isJumping", false);
            animator.SetBool("isMidAir", true);
            animator.SetBool("isFalling", false);
        }
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
