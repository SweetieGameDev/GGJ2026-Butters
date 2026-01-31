using UnityEngine;

public class EnemyBT : MonoBehaviour
{
    // rb movement variables
    [Header("Movement")]
    [SerializeField] protected float forceMultiplier = 1f;
    [SerializeField] protected Vector2 maxVelocity = new Vector2(100f, 100f);
    protected GameObject target;
    protected Rigidbody2D rb;
    protected Vector2 forceToApply;
    [HideInInspector] public Vector2 moveForce;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        NotActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Tells enemy not to do anything
    private void NotActive()
    {
        GameObject players = GameObject.FindGameObjectWithTag("Player");

        // If target isnt set or distance is lower for other player, set player as target
        if (target == null)  
        {
            target = players;
        }
    }

    // Tells enemy to be active and chase player to kill them
    public void Chasing()
    {
        if (rb.linearVelocity.x < maxVelocity.x && rb.linearVelocity.y < maxVelocity.y)
        {
            // If target is set
            if (target != null)
            {
                // Use target position and add to forceToApply
                forceToApply = ((target.transform.position - this.transform.position).normalized) * forceMultiplier;
                // Add every frame for excelleration (/100 cause too fast)
                moveForce += forceToApply / 100;
                rb.linearVelocity = moveForce;
            }
        }
    }



}
