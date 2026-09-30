using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;

    private Animator animator;
    private bool facingRight = true;
    private Rigidbody2D rigidBody;
    Vector2 playerMovementVec;

    public bool IsMoving => playerMovementVec.x != 0;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.linearVelocityX = playerMovementVec.x * playerSpeed;
    }

    private void OnMove(InputValue value)
    {

        //0 = Idle
        //1 = Run
        //2 = Jump
        //3 = MidAir
        //4 = Fall
        //Debug.Log("OnMove is being triggered");
        //Debug.Log(animator == null);
        //Debug.Log("Setting isRunning to " + (playerMovementVec.sqrMagnitude > 0.01f));
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
}
