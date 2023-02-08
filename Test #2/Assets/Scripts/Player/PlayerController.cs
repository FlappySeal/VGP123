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
    public float jumpForce;

    //groundcheck
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;


    Coroutine jumpForceChange = null;
    Coroutine speedBoost = null;

    private int _bills = 0;

    public int bills
    {
        get { return _bills; }
        set
        {
            _bills = value;

            Debug.Log("Score has been set to " + bills.ToString());
       
        }
    }

    public int maxLives = 5;
    private int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;


            Debug.Log("lives have been set to " + lives.ToString());
        }
    }



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

        if (jumpForce <= 0)
        {
            jumpForce = 350;
            Debug.Log("JumpForce was set incorrect, defaulting to " + jumpForce.ToString());

        }


        if (groundCheckRadius <= 0)
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
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;


        anime.SetFloat("hInput", Mathf.Abs(hInput));
        anime.SetBool("isGrounded", isGrounded);


        if (hInput > 0 && sp.flipX || hInput < 0 && !sp.flipX)
            sp.flipX = !sp.flipX;



    }


    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;

     }

    public void StartSpeedBoostChange()
    {
        if (speedBoost == null)
        {
            speedBoost = StartCoroutine(SpeedBoostChange());
        }
        else
        {
            StopCoroutine(speedBoost);
            speedBoost = null;
            speed /= 2;
            speedBoost = StartCoroutine(SpeedBoostChange());
        }
    }

    IEnumerator SpeedBoostChange()
    {
        speed *= 2;

        yield return new WaitForSeconds(6.0f);

        speed /= 2;
        speedBoost = null;

    }

}