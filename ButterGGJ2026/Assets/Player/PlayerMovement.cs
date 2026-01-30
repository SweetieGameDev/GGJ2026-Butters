using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;

    private float moveSpeed = 5.0f;

    private Vector2 moveInput;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocity = moveInput * moveSpeed;
    }

    //Whenever the player calls this input, they'll move left to right
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //Whenever the player calls this input, the player will jump up
    public void Jump(InputAction.CallbackContext context)
    {

    }

    public void Interact(InputAction.CallbackContext context)
    {
        
    }
}
