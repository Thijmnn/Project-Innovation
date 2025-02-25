using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private MoveBehaviour moveBehaviour;
    [SerializeField]
    private ShootingBehaviour shootBehviour;
    // Start is called before the first frame update
    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        shootBehviour = GetComponent<ShootingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
