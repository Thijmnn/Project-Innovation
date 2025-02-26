using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] float speed;
    Camera cam;
    public CoinInfo coin;
    public static event Action<CoinInfo> AddMoney;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Col");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            AddMoney?.Invoke(coin);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(0, -speed));
        if (cam.WorldToScreenPoint(transform.position).y < -Screen.width / 2)
        {

            Destroy(gameObject);
        }
    }
}
