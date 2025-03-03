using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMoveBehaviour : EnemyMoveBehaviour
{
    // Start is called before the first frame updateee;
    [SerializeField] float MoveSpeed;
    [SerializeField] float riseSpeed;

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(-MoveSpeed * orientation, GameManager.Instance.speed / riseSpeed));
        if (cam.WorldToScreenPoint(transform.position).x < -Screen.width / 1.5 || cam.WorldToScreenPoint(transform.position).x > Screen.width * 2)
        {
            Destroy(gameObject);
        }
    }
}
