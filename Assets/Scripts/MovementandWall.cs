using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementandWall : MonoBehaviour
{
    public Player2 controller;
    public Animator animatorr;

    public float runSpeed = 40f;
    public bool wallSliding;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Transform WallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
    private Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Move") * runSpeed;

        animatorr.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Up"))
        {
            jump = true;
            animatorr.SetBool("Up", true);
        }

        if (Input.GetButtonDown("Down"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Down"))
        {
            crouch = false;
        }

        
    }
    public void OnLanding()
    {
        animatorr.SetBool("Up", false);

    }

    public void OnCrouching(bool Down)
    {
        animatorr.SetBool("Down", Down);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
