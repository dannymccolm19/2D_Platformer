using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        PlayerController p = other.collider.GetComponent<PlayerController>();
        if (p != null)
        {
            
            p.ChangeHealth();
        }
        
    }
}
