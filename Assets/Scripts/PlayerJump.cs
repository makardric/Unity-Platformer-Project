using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerJump : MonoBehaviour
{
    
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Collider2D groundTriggerCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource jumpAudio;
    // small buffer so you can't instant double jump
    [SerializeField] private float doubleJumpDelay = 0.5f;
    private float firstJumpTime;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private bool onGround = true;
    private bool jumped = false;
    //0 = Idle
    //1 = Run
    //2 = Jump
    //3 = MidAir
    //4 = Fall


    
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
        onGround = groundTriggerCollider.IsTouchingLayers(groundLayer);

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

    //Debug.Log($"onGround={onGround}, velY={rigidBody.linearVelocityY}, state={state}");
    animator.SetInteger("AnimationState", state);
    }

    private void OnJump(InputValue value)
    {
        //Debug.Log("am i mutted chat");
        // if on ground
        // different amounts of time W is held = different force exerted on character
        if (value.isPressed)
        {
            if (onGround) {
                rigidBody.AddForce(Vector2.up * jumpForce);
                onGround = false;
                jumped = true;
                firstJumpTime = Time.time;
                jumpAudio.Play();
            }
            else if (jumped && Time.time >= firstJumpTime + doubleJumpDelay) {
                rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0);
                rigidBody.AddForce(Vector2.up * jumpForce);
                jumped = false;
                jumpAudio.Play();
            }
                
        }
    }
}
