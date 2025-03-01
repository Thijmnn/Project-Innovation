using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBehaviour : EnemyMoveBehaviour
{
    [SerializeField] float fallingSpeed;

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(0, -fallingSpeed * GameManager.Instance.jetMult * orientation));
        if (cam.WorldToScreenPoint(transform.position).y < -Screen.height / 2)
        {
            Destroy(gameObject);
        }
    }
}
