using UnityEngine;

public class blockscr : MonoBehaviour
{
    private Rigidbody2D rb;
    private float touching;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touching = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided with: " + collision.gameObject.name);
        //rb.linearVelocity = Vector2.zero;
        touching++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //rb.linearVelocity = Vector2.zero;
        touching--;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = GravityManager.terminalV(rb.linearVelocity);
        if (touching > 0)
        {
            rb.linearVelocity = GravityManager.getVComp(rb.linearVelocity);
        }
    }
}
