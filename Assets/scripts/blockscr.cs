using UnityEngine;

public class blockscr : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.name);
        rb.linearVelocity = Vector2.zero;
        /*
        if (collision.collider.CompareTag("ground"))
        {
            Debug.Log("hit ground");
            return;
        }
        Touch++;
        Debug.Log("Touch: " + Touch);
        */
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.linearVelocity = Vector2.zero;
        /*
        if (collision.collider.CompareTag("ground"))
        {
            return;
        }
        Touch--;
        Debug.Log("Touch: " + Touch);
        */
    }

    /*
    void FixedUpdate()
    {
        if (Touch > 0)
        {
                rb.linearVelocity = Vector2.zero;
        }
    }
    */
}
