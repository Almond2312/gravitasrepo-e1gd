using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
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
    [SerializeField] Transform groundCheckL;
    [SerializeField] Transform groundCheckR;
    [SerializeField] float groundRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] float coyote = .1f;
    float timer;

    // Checkpoint
    public UnityEvent playerDeath;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        /*
        Debug.DrawLine(//shows feet location
            transform.position,
            groundCheck.position,
            Color.red
        );
        Debug.DrawRay(//shows where relative down is
            groundCheck.position,
            GravityManager.Instance.relativeDown,
            Color.blue
        );
        */
        //horizontal motion, horizontal is calculated, vertical is same
        
        rb.linearVelocity = (GravityManager.Instance.relativeRight * (movex * speed)) + (GravityManager.Instance.relativeDown * Vector2.Dot(rb.linearVelocity, GravityManager.Instance.relativeDown));
        rb.linearVelocity = GravityManager.terminalV(rb.linearVelocity);
        //fancy way to do grounding according to ai
        grounded = Physics2D.OverlapCircle(groundCheckL.position, groundRadius, groundLayer) || Physics2D.OverlapCircle(groundCheckR.position, groundRadius, groundLayer);
        if (grounded)//look at later
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
            Vector2 jd = GravityManager.Instance.relativeUp;
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
        //gets radian angle of the new down and makes it degrees.
        float angle = Mathf.Atan2(GravityManager.Instance.relativeUp.y, GravityManager.Instance.relativeUp.x) * Mathf.Rad2Deg - 90f;

        //rotates the player instantly to gravity angle
        //darker than black magic, practically necromancy
        //unity uses quaternions, quaternion.euler is just putting the sensible angle from earlier into the nonsense
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}