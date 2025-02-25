using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed;

    private Rigidbody2D rb;
    private float speedX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        LeftToRight();
    }

    void LeftToRight()
    {
        //Movement
        speedX = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(speedX, 0);
        
        //Teleporting from side to side when outside of the screen
        if((transform.position.x + transform.localScale.x/2) <= -3)
        {
            transform.position = new Vector3(3 + transform.localScale.x/3,transform.position.y, transform.position.z);
        }
        if ((transform.position.x - transform.localScale.x / 2) >= 3)
        {
            transform.position = new Vector3(-3 - transform.localScale.x / 3, transform.position.y, transform.position.z);
        }
    }
}
