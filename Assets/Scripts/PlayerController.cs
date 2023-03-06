using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public int jumpsAmount;
    int jumpsLeft;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    public int maxJet;
    int jet;

    bool isGrounded;

    float moveInput;
    float jetInput;
    Rigidbody2D rb2d;
    Animator anim;
    ParticleSystem jetSmoke;
    float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jetSmoke = GetComponent<ParticleSystem>();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        jetInput = Input.GetAxisRaw("Vertical");

        Jump();
        
        
        //CheckIfGrounded();
        //anim.SetBool("Jumping", !isGrounded); 
        
    }

    private void FixedUpdate()
    {
        Move();
        if(Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer)){
              anim.SetBool("Jet", false);
        }
        
    }

    public void Move()
    {
        Flip();
        if (Input.GetKey(KeyCode.UpArrow) && jet !=0)
        {
            anim.SetBool("Jet", true);
            jetSmoke.Play();
            rb2d.velocity = new Vector2(rb2d.velocity.x, jetInput* moveSpeed);
            jet--;
            Debug.Log(jet);
                               
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer)){
            jetSmoke.Pause();
            jetSmoke.Clear();
            rb2d.velocity = new Vector2(moveInput * 1.5f*moveSpeed, rb2d.velocity.y);    
        }
        else{
            jetSmoke.Pause();
            jetSmoke.Clear();
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
        
    }

    public void Flip()
    {
        if (moveInput > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)){
                anim.SetBool("Running", true);    
            }
            else{
                anim.SetBool("Running", false);
                anim.SetBool("Moving", true); 
            }
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
           
        }
        if (moveInput < 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)){
                anim.SetBool("Running", true);    
            }
            else{
                anim.SetBool("Running", false);
                anim.SetBool("Moving", true); 
            }
            transform.localScale = new Vector3((-1)*scaleX, transform.localScale.y, transform.localScale.z);
            
          
           
        }
        if(moveInput == 0){
            anim.SetBool("Moving", false); 
            anim.SetBool("Running", false); 
        }
    }

    public void Jump()
    {
        //CheckIfGrounded();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckIfGrounded();
            if (jumpsLeft > 0)
            {
                anim.SetTrigger("Jumping"); 
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpsLeft--;
            }
                               
        }
        
        
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer);
        ResetJumps();
    }

    public void ResetJumps()
    {
        if (isGrounded)
        {
            jumpsLeft = jumpsAmount;// jumpsAmount =2;
        }
    }

    public void RefillJetpack()
    {
        jet = maxJet;
    }

    
}