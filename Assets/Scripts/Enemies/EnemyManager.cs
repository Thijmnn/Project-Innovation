using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMoveBehaviour))]
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private EnemyMoveBehaviour moveBehaviour;
    [SerializeField]
    private ShootingBehaviour shootBehviour;
    // Start is called before the first frame update
    void Start()
    {
        moveBehaviour = GetComponent<EnemyMoveBehaviour>();
        shootBehviour = GetComponent<ShootingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
