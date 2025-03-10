using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBehaviour : MonoBehaviour
{
    // Start is called before the first frame updateee
    protected Camera cam;
    protected Rigidbody2D rb;
    public int orientation;
    void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x * orientation, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, 0.1f);
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

}
