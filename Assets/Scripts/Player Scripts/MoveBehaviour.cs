using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed;

    private Rigidbody2D rb;
    private float speedX;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, 0.1f);
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
        Vector3 cPos = cam.WorldToScreenPoint(transform.position);
        if (cPos.x + 20 <= 0)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Screen.width, cPos.y, cPos.z));
        }
        if (cPos.x - 20 >= Screen.width)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(0, cPos.y, cPos.z));
        }
    }
}
