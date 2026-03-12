using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Collections;
//can interact with everything, slightly less than 1x1 tile, checkpoint code is also in here

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
    // public UnityEvent playerDeath;
    [SerializeField] float cutcorner = .15f;
    Vector2 checkpointPosition;
    [SerializeField] float respawnOffset = 1f;
    Vector2 currentRespawnAnchor;
    Vector2 dist;
    bool canMove = true;
    GameObject touchingCheckpoint;
    GameObject prevCheckpoint;

    // Audio
    public AudioClip Death_Sound;
    public AudioClip Background_Music;
    private AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        checkpointPosition = transform.position;

        //Background Music
        //audioSource.clip = Background_Music;
        audioSource.loop = true;
        audioSource.Play();
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

        if (touchingCheckpoint != null && touchingCheckpoint != prevCheckpoint)
        {
            dist = touchingCheckpoint.transform.position - transform.position;
            if (Mathf.Abs((Mathf.Abs(dist.x) - Mathf.Abs(dist.y))) >= cutcorner)
            {
                prevCheckpoint = touchingCheckpoint;
                checkpointPosition = GravityManager.Instance.relativeUp * respawnOffset;
                currentRespawnAnchor = touchingCheckpoint.transform.position + new Vector3(checkpointPosition.x, checkpointPosition.y, 0);
                Debug.Log("checkpointspot: " + touchingCheckpoint.transform.position);
                Debug.Log("relativespot: " + (Vector2)touchingCheckpoint.transform.position);
                Debug.Log("relativedirect: " + (currentRespawnAnchor - (Vector2)touchingCheckpoint.transform.position));
            }
        }
    }

    IEnumerator RespawnFreeze()
    {
        canMove = false;          // your movement script checks this
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    void OnMove(InputValue value)
    {
        if (!canMove) return;
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

    public void SetCheckpoint(Vector2 basePosition)
    {
        checkpointPosition = basePosition + (GravityManager.Instance.relativeUp * respawnOffset);
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = currentRespawnAnchor;
        StartCoroutine(RespawnFreeze());
        /*Vector2 targetPos;
        if (currentRespawnAnchor == null)
        {
            targetPos = checkpointPosition;
        }
        else
        {
            //targetPos = (Vector2)currentRespawnAnchor.transform.position + (Vector2)(GravityManager.Instance.relativeUp * respawnOffset);
        }//relative up will be a unit vector, so * respawn offset is normal, adding it to the position of the block results in respawn on that face.
        rb.linearVelocity = Vector2.zero;
        transform.position = currentRespawnAnchor;

        // Check if the spot is free
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.3f, LayerMask.GetMask("ground"));
        if (hit != null)
        {
            // if spawn is blocked, respawn to beginning
            targetPos = checkpointPosition;
            rb.linearVelocity = Vector2.zero;
            transform.position = targetPos;
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player touches death
        if (other.CompareTag("Death"))
        {
            audioSource.PlayOneShot(Death_Sound);
            Respawn();
        }
        // If player touches Repawn
        if (other.CompareTag("Respawn"))
        {
            touchingCheckpoint = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Respawn"))
            {
                touchingCheckpoint = null;
            }
    }
}