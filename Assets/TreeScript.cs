using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : FallBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (orientation == 1)
        {
            transform.position = new Vector3(transform.position.x - -transform.localScale.x*10, transform.position.y,transform.position.z);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + -transform.localScale.x*10, transform.position.y, transform.position.z);
            
        }
    }

}
