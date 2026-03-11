using UnityEngine;
//pushable by the player. can hit buttons, affected by changing gravity, does not affect grav changers
//slightly less than 1x1 tile

public class blockscr : MonoBehaviour
{
    private Rigidbody2D rb;
    private int touching;
    Collision2D c;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touching = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        c = collision;
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
        //Debug.Log(rb.linearVelocity);
        rb.linearVelocity = GravityManager.terminalV(rb.linearVelocity);
        if (touching > 0)
        {
            rb.linearVelocity = GravityManager.getVComp(rb.linearVelocity);
        }
    }
}
