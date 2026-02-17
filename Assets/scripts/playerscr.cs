using UnityEngine;
using UnityEngine.InputSystem;
//testpush
public class playerscr : MonoBehaviour
{
    //movement script
    //add momentum, feels better to move with
    //scale up the map size later so more tiles fit on screen, adjust movement accordingly

    //walk
    float movex;
    float movey;
    [SerializeField] float speed = 5f;
    
    Rigidbody2D rb;

    //yump
    [SerializeField] float jump = 5f;
    bool grounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] float coyote = .1f;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 down = GravityManager.Instance.relativeDown;
        Vector2 right = Vector2.Perpendicular(down);

        Vector2 h = right * (movex * speed);
        Vector2 v = down * Vector2.Dot(rb.linearVelocity, down);

        rb.linearVelocity = h + v;

        //regular walk
        //rb.linearVelocity = new Vector2(movex * speed, rb.linearVelocity.y);

        //grounded needs fixing with various orientations
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (grounded)
        {
            timer = coyote;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    void Update()
    {
        //yump
        if (timer > 0f && movey > 0)
        {
            Vector2 jd = -GravityManager.Instance.relativeDown;
            rb.linearVelocity = jd * jump;
        }
        AlignToGravity();
        timer = 0f;
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movex = v.x;
        movey = v.y;
    }

    void AlignToGravity()
    {
        //current relative down
        Vector2 down = GravityManager.Instance.relativeDown;

        //gets radian angle and makes degrees, then finally adds 90 since tan^-1 is rotated
        float angle = Mathf.Atan2(down.y, down.x) * Mathf.Rad2Deg + 90f;

        //darker than black magic, practically necromancy
        //unity uses quaternions, quaternion.euler is just putting the sensible angle from earlier into the nonsense
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
