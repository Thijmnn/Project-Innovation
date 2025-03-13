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
        if (!GameManager.Instance.gameOn)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        if (timer > despawnTime)
        {
            Destroy(gameObject);
        }
    }
}
