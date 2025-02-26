using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBehaviour : MonoBehaviour
{
    // Start is called before the first frame updateee
    Camera cam;
    [SerializeField] float fallingSpeed;
    Rigidbody2D rb;
    [SerializeField] float PercentageOfScreen = 1000;
    void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, 0.1f);
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
