using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;

    private Animator animator;
    private bool facingRight = true;

    Vector2 playerMovementVec;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        gameObject.transform.Translate(playerMovementVec * playerSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        //Debug.Log("OnMove is being triggered");
        //Debug.Log(animator == null);
        //Debug.Log("Setting isRunning to " + (playerMovementVec.sqrMagnitude > 0.01f));
        playerMovementVec = value.Get<Vector2>();
        if(playerMovementVec.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // if the movement vector is -1 on the x (moving left)
        if (playerMovementVec.x == -1)
        {
            facingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        // if the movement vector is 1 on the x (moving right)
        else if (playerMovementVec.x == 1 && facingRight == false)
        {
            facingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;

        }
    }
}
