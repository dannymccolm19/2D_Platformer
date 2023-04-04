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
    public LayerMask WaterLayer;
    public LayerMask LadderLayer;

    public int maxJet;
    int jet;
    bool underwater = false;
    bool climb = false;

    bool isGrounded;

    float moveInput;
    float jetInput;
    Rigidbody2D rb2d;
    Animator anim;
    ParticleSystem jetSmoke;
    float scaleX;
    ContactFilter2D waterFilter = new ContactFilter2D();
    ContactFilter2D ladderFilter = new ContactFilter2D();
    Collider2D[] waterColliders = new Collider2D[16];
    Collider2D[] ladderColliders = new Collider2D[16];
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jetSmoke = GetComponent<ParticleSystem>();
        scaleX = transform.localScale.x;

        waterFilter.SetLayerMask(WaterLayer);
        waterFilter.useLayerMask = true;
        ladderFilter.SetLayerMask(LadderLayer);
        ladderFilter.useLayerMask = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Underwater", underwater);
        anim.SetBool("Climb", climb);
        moveInput = Input.GetAxisRaw("Horizontal");
        jetInput = Input.GetAxisRaw("Vertical");

        Jump(); 
        
    }

    private void FixedUpdate()
    {
        Move();
        if(rb2d.OverlapCollider(waterFilter,waterColliders)>0){
              anim.SetBool("Jet", false);
              underwater = true;
              climb = false;
        }
        else if(rb2d.OverlapCollider(ladderFilter,ladderColliders)>0){
            anim.SetBool("Jet", false);
            climb = true;
            underwater = false;
        }
        else if(Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer)){
              anim.SetBool("Jet", false);
              underwater = false;
              climb = false;
        }
        else{
            underwater = false;
            climb = false;
        }
        
    }

    public void Move()
    {
        Flip();
        if(underwater){ 
            rb2d.velocity = new Vector2(moveInput * moveSpeed*0.5f, jetInput * moveSpeed*0.3f);
        }
        else if(Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, WaterLayer)){
              rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce*jetInput);
        }
        else if(climb){ 
            rb2d.velocity = new Vector2(moveInput * moveSpeed, jetInput * moveSpeed*0.7f);
            if(!Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer)){
                rb2d.velocity = new Vector2(0f, jetInput * moveSpeed*0.7f);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && jet !=0)
        {
            anim.SetBool("Jet", true);
            jetSmoke.Play();
            rb2d.velocity = new Vector2(rb2d.velocity.x, jetInput* moveSpeed);
            jet--;
            //Debug.Log(jet);
                               
        }
        else{
            if (Input.GetKey(KeyCode.DownArrow)){
                anim.SetTrigger("Slide");
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer)){
                jetSmoke.Pause();
                jetSmoke.Clear();
                rb2d.velocity = new Vector2(moveInput * 1.5f*moveSpeed, rb2d.velocity.y); 
                anim.ResetTrigger("Slide");    
            }
            else{
                jetSmoke.Pause();
                jetSmoke.Clear();
                rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
                anim.ResetTrigger("Slide"); 
            }
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