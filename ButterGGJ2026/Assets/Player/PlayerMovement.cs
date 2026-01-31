using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public CapsuleCollider2D cc2d;

    #region [Animation]

    private Animator playerAnimator;

    private bool isWalking;

    private bool isRunning;

    private bool isJumping;

    #endregion

    #region [Movement]

    public float moveSpeed;

    public float jumpSpeed;

    private bool isJumping;

    private Vector2 moveInput;

    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        isJumping = false;

        playerAnimator = GetComponent<Animator>();

        playerAnimator.SetBool("isIdle", true);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isInteracting", false);
        playerAnimator.SetBool("isPushing", false);
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isDeath", false);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.linearVelocity.y);
    }


    #region [Funtions]

    //Whenever the player calls this input, they'll move left to right
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        playerAnimator.SetBool("isIdle", false);
        playerAnimator.SetBool("isWalking", true);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isInteracting", false);
        playerAnimator.SetBool("isPushing", false);
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isDeath", false);
        Debug.Log("Move triggered");
    }

    //Whenever the player calls this input, the player will jump up
    public void Jump(InputAction.CallbackContext context)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (!isJumping)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);

            isJumping = true;

            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isInteracting", false);
            playerAnimator.SetBool("isPushing", false);
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isDeath", false);
        }
        Debug.Log("Jump triggered");
    }

    public void Interact(InputAction.CallbackContext context)
    {
        playerAnimator.SetBool("isIdle", false);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isInteracting", true);
        playerAnimator.SetBool("isPushing", false);
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isDeath", false);

        Debug.Log("Interact triggered");
        Debug.Log(context);
    }

    public void DropDownPlatform(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            cc2d.enabled = false;
        }
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Not jumping triggered");
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }
}
