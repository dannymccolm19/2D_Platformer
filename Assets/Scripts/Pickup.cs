using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool jet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.GetComponent<PlayerController >();
        if (p != null)
        {
            if(jet){
                p.RefillJetpack();
            }
            else{
                p.IncreaseScore();
            }
            gameObject.SetActive(false);

        }
        //Console.WriteLine("Hello!");
        

    }
}
