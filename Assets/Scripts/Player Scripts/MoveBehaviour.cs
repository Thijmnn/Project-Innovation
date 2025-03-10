using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveBehaviour : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float jetMult;
    public static event Action beginGame;
    bool boosting;
    [SerializeField] float moveSpeed;
    public float speedCharge;
    [SerializeField] GameObject playerLauncher;

    private float startRot;
    Quaternion offset;

    float xVelocity;
    float yVelocity;

    float speedX;
    
    float launchSpeed;

    private Rigidbody2D rb;

    bool launched;

    Camera cam;

    [SerializeField] bool isOnPhone;
    public Vector3 startingPhoneRotation;
// Start is called before the first frame update
void Start()
{
    startingPhoneRotation = new Vector3(90, 0, 0);
    offset = Quaternion.Inverse(GyroToUnity(Input.gyro.attitude));
    cam = Camera.main;
    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, -0.5f);
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
    transform.position = playerLauncher.transform.position;
}

private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

// Update is called once per frame
void Update()
{
     if (isOnPhone)
     {
        if (launched) { LeftToRight(); }
        else { /*DragLaunch();*/ HoldLaunch(); }
     }
     else
     {
        LeftToRight();
     }

}

private void FixedUpdate()
{
    if (isOnPhone)
    {
       rb.velocity = new Vector2(xVelocity, yVelocity);
    }
    else
    {
       rb.velocity = new Vector2(speedX, 0);
    }
}

void LeftToRight()
{
    //Movement
    xVelocity = Input.acceleration.x * moveSpeed;
    Quaternion phoneRot = offset * GyroToUnity(Input.gyro.attitude);
    yVelocity = phoneRot.x * moveSpeed;
    
    speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    
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
    private void HoldLaunch()
    {
        //HOLDING DOWN ON THE SCREEN
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Stationary)
        {
            launchSpeed += speedCharge;
            //JETPACK
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            LaunchPlayer(launchSpeed);
            launched = true;
            launchSpeed = 0;

        }
    }
    private void DragLaunch() { 
        //DRAGGING DOWN
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            Vector3 fingerPos = cam.ScreenToWorldPoint(Input.touches[0].position);
            if (fingerPos.y <= playerLauncher.transform.position.y)
            {
                Vector3 draggedPos = transform.position = cam.ScreenToWorldPoint(new Vector3(0, Input.touches[0].position.y,0));
                transform.position = new Vector3(playerLauncher.transform.position.x, draggedPos.y, playerLauncher.transform.position.z);  
            }
            else
            {
                transform.position = playerLauncher.transform.position;
            }

        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            float dist = transform.position.y - playerLauncher.transform.position.y;
            LaunchPlayer(-dist * speedCharge);
            launched = true;
            launchSpeed = 0;

            //Temp
            transform.position = playerLauncher.transform.position;
        }
    }
    private void LaunchPlayer(float speedCharge) 
    {
        /*transform.position = cam.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y,0));
        transform.position = new Vector3(transform.position.x,transform.position.y,0);*/
        GameManager.Instance.speed = speedCharge;
        beginGame?.Invoke();
        print(speedCharge);
        
    }
    
}
