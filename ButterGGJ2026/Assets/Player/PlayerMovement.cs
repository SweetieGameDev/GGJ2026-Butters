using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;

    #region [Animation]

    private bool isIdle;

    private bool isWalking;

    private bool isRunning;

    #endregion

    #region [Movement]

    public float moveSpeed;

    public float jumpSpeed;

    private Vector2 moveInput;

    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        isIdle = true;

        isWalking = false;

        isRunning = false;

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
        isIdle = false;
        isWalking = false;
        isRunning = false;
        Debug.Log("Move triggered");
    }

    //Whenever the player calls this input, the player will jump up
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
        }
        Debug.Log("Jump triggered");
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact triggered");
    }
    #endregion
}
