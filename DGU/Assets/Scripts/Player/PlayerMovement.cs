using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //move variables
    private float horizontalAxis;
    public float moveSpeed;
    public bool facingRight = true;
    private Animator anim;

    //jump variables
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce = 5;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isGrounded;
    private bool isJumping;
    private bool isJumpingLow;

    public Rigidbody2D playerRB;

    public PlayerStats playerStats;
    private void Awake()
    {
        SoundManager.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAxis();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (!playerStats.isDead)
        {
            Move(horizontalAxis, playerRB, moveSpeed);
            Jump();
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }
    }

    private void Jump()
    {
        if (isJumping && isGrounded)
        {
            playerRB.velocity = Vector2.up * jumpForce;
        }
        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y > 0 && !isJumpingLow)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void CheckAxis()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetButtonDown("Jump");
        isJumpingLow = Input.GetButton("Jump");

        if (horizontalAxis !=0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void Move(float horizontal, Rigidbody2D rb, float speed)
    {
        if(!isJumping && !isJumpingLow && horizontalAxis !=0 && !PauseMenu.gamePaused)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerMove, groundCheck.transform.position);
        }
        
        rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        if(!PauseMenu.gamePaused)
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
    
    public void AddMoveSpeed()
    {
        if(PlayerStats.souls >=1)
        {
            moveSpeed += 10;
            PlayerStats.souls -= 1;
        }
        
    }
    
    public void AddJumpForce()
    {
        if (PlayerStats.souls >= 1)
        {
            jumpForce += 0.1f;
            PlayerStats.souls -= 1;
        }
       
    }

}
