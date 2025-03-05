using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnScript : MonoBehaviour
{
    [SerializeField] float despawnTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > despawnTime)
        {
            Destroy(gameObject);
        }
    }
}
