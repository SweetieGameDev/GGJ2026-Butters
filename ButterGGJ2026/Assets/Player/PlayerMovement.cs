using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public CapsuleCollider2D cc2d;

    private bool maskNear = false;
    private GameObject maskObj;

    #region [Animation]

    private Animator playerAnimator;

    private bool isWalking;

    private bool isRunning;

    private bool isJumping;

    #endregion

    #region [Movement]

    public float moveSpeed;

    public float jumpSpeed;

    private bool touchingPlatform;

    private bool isDead;

    private Vector2 moveInput;

    #endregion

    #region [Audio]

    public AudioSource dollBreak;

    public List<AudioSource> walkSFX;

    #endregion



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        isJumping = false;
        touchingPlatform = false;

        playerAnimator = GetComponent<Animator>();

        playerAnimator.SetBool("isIdle", true);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isInteracting", false);
        playerAnimator.SetBool("isPushing", false);
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isDeath", false);

        //walkSFX = GetComponent<List<AudioSource>>();
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
        if (!isDead)
        {
            moveInput = context.ReadValue<Vector2>();
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isInteracting", false);
            playerAnimator.SetBool("isPushing", false);
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isDeath", false);

            int rand = Random.Range(0, 3);
            walkSFX[rand].Play();
        }
    }

    //Whenever the player calls this input, the player will jump up
    public void Jump(InputAction.CallbackContext context)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (!isJumping && !isDead)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);

            isJumping = true;
            touchingPlatform = false;

            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isInteracting", false);
            playerAnimator.SetBool("isPushing", false);
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isDeath", false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!isDead && maskNear)
        {
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isInteracting", true);
            playerAnimator.SetBool("isPushing", false);
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isDeath", false);

            // Run mask interact
            Destroy(maskObj);
        }
    }

    public void DropDownPlatform(InputAction.CallbackContext context)
    {
        if (!isJumping && touchingPlatform)
        {
            cc2d.enabled = false;
            isJumping = true;
            StartCoroutine(EnableCollision());
        }
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.5f);
        cc2d.enabled = true;
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            touchingPlatform = false;
        }

        if (collision.gameObject.tag == "Platform")
        {
            touchingPlatform = true;
        }

        //If the enemy touches the player, they will die and the game will restart
        if (collision.gameObject.tag == "Enemy")
        {
            isDead = true;

            StartCoroutine(WaitForAnimation());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.ToString() == "7")
        {
            maskNear = true;
            maskObj = collision.gameObject;
        }
    }

    private IEnumerator WaitForAnimation()
    {
        playerAnimator.SetBool("isIdle", false);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isInteracting", false);
        playerAnimator.SetBool("isPushing", false);
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isDeath", true);

        //Play doll breaking sound effect
        dollBreak.Play();

        // Wait for one frame to ensure that the animation has started
        yield return null;

        // Get the length of the current animation, which will be "isDeath"
        float animationLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;

        // Wait for the duration of the enemy death animation
        yield return new WaitForSeconds(animationLength);

        //Animation is done destory self
        StopCoroutine(WaitForAnimation());
    }
}
