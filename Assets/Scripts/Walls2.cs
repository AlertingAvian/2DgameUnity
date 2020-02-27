using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walls2 : MonoBehaviour
{

    public float maxSpeed = 10f;
    bool facingRight = true;
    public Animator anim;
   
    //sets up the grounded stuff
    bool grounded = false;
    bool touchingWall = false;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform wallCheck2;
    float groundRadius = 0.2f;
    float wallTouchRadius = 0.2f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public float jumpForce = 700f;
    public float jumpPushForce = 700f;
    bool crouch = false;

    float horizontalMove = 0f;
    //double jump
    bool doubleJump = false;
   
  

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        crouch = Input.GetButton("Down");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Move") * maxSpeed;
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        touchingWall = Physics2D.OverlapCircle(wallCheck.position, wallTouchRadius, whatIsWall);
        touchingWall = Physics2D.OverlapCircle(wallCheck2.position, wallTouchRadius, whatIsWall);
        anim.SetBool("Ground", grounded);

        if (grounded)
        {
            doubleJump = false;
            anim.SetBool("Up", false);
        }

        if (touchingWall)
        {
            grounded = false;
            doubleJump = false;
        }

        anim.SetFloat("speed", GetComponent<Rigidbody2D>().velocity.y);



        float move = Input.GetAxis("Move");
            anim.SetFloat("speed", Mathf.Abs(horizontalMove));
        
        anim.SetFloat("speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (crouch && grounded)
        {
            crouch = true;
           
        }
        else
        {
            crouch = false;
        }
        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }// Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    void Update()
    {

        // If the jump button is pressed and the player is grounded then the player should jump.
        if ((grounded || !doubleJump) && Input.GetButtonDown("Up"))
        {
            anim.SetBool("Ground", false);
            anim.SetBool("Up", true);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
                Debug.Log("DoubleJump");
            }

        }
     
        if (touchingWall && Input.GetButtonDown("Up"))
        {
            WallJump();
            
        }


        void WallJump()
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpPushForce, jumpForce));
            Debug.Log("WallJumped");
        }

    }
    
    void Flip()
        {

            // Switch the way the player is labelled as facing
            facingRight = !facingRight;

            //Multiply the player's x local cale by -1
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    
}
