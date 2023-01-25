using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{


    //Components
    Rigidbody2D rb;
    Animator anime;
    SpriteRenderer sp;


    //Movement var
    public float speed;
    public float JumpForce;

    //groundcheck
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    
        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrect, defaulting to " + speed.ToString());

        }

       if(JumpForce <= 0)
        {
            JumpForce = 300;
            Debug.Log("JumpForce was set incorrect, defaulting to " + JumpForce.ToString());

        }


       if(groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("Ground Check Radius was set incorrect, defaulting to " + groundCheckRadius.ToString());

        }

       if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check was not set, finding it manually!");

        }


    }


    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);


        if (Input.GetButtonDown("Fire1"))
        {
            anime.SetTrigger("Attack");
        }


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * JumpForce);
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;


        anime.SetFloat("hInput", Mathf.Abs(hInput));
        anime.SetBool("isGrounded", isGrounded);
    }
}
