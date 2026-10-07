using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private Collider2D groundTriggerCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask deathLayer;
    [SerializeField] private GameObject campfire;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource runningAudio;
    // small buffer so you can't instant double jump
    [SerializeField] private float doubleJumpDelay = 0.5f;

    private bool fell = false;

    private Animator animator;
    private Rigidbody2D rigidBody;

    private bool facingRight = true;
    private Vector2 playerMovementVec;

    private float firstJumpTime;

    private bool onGround = true;
    private bool jumped = false;
    //0 = Idle
    //1 = Run
    //2 = Jump
    //3 = MidAir
    //4 = Fall

    public bool IsMoving => playerMovementVec.x != 0;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = playerMovementVec.x * playerSpeed;

        // jumping range ( > 0)
        // mid air range (-0.5 - 0.5)
        // falling range ( < 0)
        onGround = groundTriggerCollider.IsTouchingLayers(groundLayer);
        fell = groundTriggerCollider.IsTouchingLayers(deathLayer);


        if (fell)
        {
            gameObject.transform.position = campfire.transform.position;
            fell = false;
        }
        int state;

        if (onGround)
        {
            //Debug.Log("Ground being touched");
            if (IsMoving)
            {
                // run
                state = 1;
                if (!runningAudio.isPlaying)
                    runningAudio.Play();
            }
            else
            {
                // idle
                state = 0;
                if (runningAudio.isPlaying)
                    runningAudio.Stop();
            }
        }
        else
        {
            if (runningAudio.isPlaying)
                runningAudio.Stop();

            if (rigidBody.linearVelocityY > 0.5f)
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
        }

        //Debug.Log($"onGround={onGround}, velY={rigidBody.linearVelocityY}, state={state}");
        animator.SetInteger("AnimationState", state);
    }

    private void OnMove(InputValue value)
    {
        //0 = Idle
        //1 = Run
        //2 = Jump
        //3 = MidAir
        //4 = Fall
        //Debug.Log("OnMove is being triggered");
        playerMovementVec = value.Get<Vector2>();

        // if the movement vector is -1 on the x (moving left)
        if (playerMovementVec.x == -1)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // if the movement vector is 1 on the x (moving right)
        else if (playerMovementVec.x == 1 && facingRight == false)
        {
            facingRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnJump(InputValue value)
    {
        //Debug.Log("am i mutted chat");
        // if on ground
        // different amounts of time W is held = different force exerted on character
        if (value.isPressed)
        {
            if (onGround)
            {
                rigidBody.AddForce(Vector2.up * jumpForce);
                onGround = false;
                jumped = true;
                firstJumpTime = Time.time;
                jumpAudio.Play();
            }
            else if (jumped && Time.time >= firstJumpTime + doubleJumpDelay)
            {
                rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0);
                rigidBody.AddForce(Vector2.up * jumpForce);
                jumped = false;
                jumpAudio.Play();
            }
        }
    }
}