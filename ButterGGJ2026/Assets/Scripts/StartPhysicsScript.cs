using UnityEngine;

public class StartPhysicsScript : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Player")
        {
            rb.gravityScale = 2;
        }
    }
}
