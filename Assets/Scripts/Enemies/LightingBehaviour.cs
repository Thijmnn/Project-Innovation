using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : EnemyMoveBehaviour
{

    private void Start()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
    }
}
