using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRb;
    float x;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if(x>0)
        {
            myRb.velocity = Vector2.right * speed * Time.deltaTime;
        } 
        else if(x <0)
        {
            myRb.velocity = Vector2.left * speed * Time.deltaTime;
        }
        else
        {
            myRb.velocity = Vector2.zero;
        }
    }
}
