using UnityEngine;

public class blockscr : MonoBehaviour
{
    private Rigidbody2D rb;
    private int Touch = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            return;
        }
        Touch++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Touch--;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Touch > 0)
        {
                rb.linearVelocity = Vector2.zero;
        }
    }
}
