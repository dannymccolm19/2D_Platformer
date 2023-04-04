using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool moves;
    public float moveSpeed;

    Rigidbody2D rb2d;
    Animator anim;
    float scaleX = 1;

    public float limit = 4f;
    float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Move", moves);
        //scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(moves){
            counter += Time.deltaTime;
            if(counter > limit){
                scaleX = scaleX*-1f;
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
                counter = 0f;
            }
            rb2d.velocity = new Vector2(scaleX * -1f * moveSpeed, rb2d.velocity.y);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        PlayerController p = other.collider.GetComponent<PlayerController>();
        if (p != null)
        {
            anim.SetTrigger("Attack");
            //t.ChangeHealth(damage);
        }
        
    }
}
