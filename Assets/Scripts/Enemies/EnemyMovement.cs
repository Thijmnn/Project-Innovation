using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame updateee
    Camera cam;
    [SerializeField] float fallingSpeed;
    Rigidbody2D rb;
    void Start()
    {
        print("spawned");
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(0,-fallingSpeed));
        if (cam.WorldToScreenPoint(transform.position).y < -Screen.width/2)
        {
            
            Destroy(gameObject);
        }
    }
}
