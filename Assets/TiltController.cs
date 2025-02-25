using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 20f;
    float xVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xVelocity = Input.acceleration.x * speed;
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xVelocity,0);
    }
}
