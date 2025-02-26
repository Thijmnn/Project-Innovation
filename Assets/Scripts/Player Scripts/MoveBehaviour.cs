using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [Header("Stats")]
    public float speed;

float xVelocity;
float speedX;

private Rigidbody2D rb;

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
    TouchCheck();
}

private void FixedUpdate()
{
    rb.velocity = new Vector2(xVelocity, 0);
    rb.velocity = new Vector2(speedX, 0);
}

void LeftToRight()
{
    //Movement
    xVelocity = Input.acceleration.x * speed;

    speedX = Input.GetAxisRaw("Horizontal") * speed;

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
private void TouchCheck()
{
    if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                hit.collider.GetComponent<MeshRenderer>().material.color = Color.white;
                Debug.Log("skibidy");
                LaunchPlayer();
            }
        }
    }
}
private void LaunchPlayer()
{

    transform.position = Input.touches[0].position;
}
    
}
