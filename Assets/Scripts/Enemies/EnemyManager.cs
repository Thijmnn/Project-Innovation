using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

[RequireComponent(typeof(EnemyMoveBehaviour))]
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private EnemyMoveBehaviour moveBehaviour;
    [SerializeField]
    private ShootingBehaviour shootBehviour;
    [SerializeField]
    private AudioSource collisonSound;
    // Start is called before the first frame update
    void Start()
    {
        moveBehaviour = GetComponent<EnemyMoveBehaviour>();
        shootBehviour = GetComponent<ShootingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOn)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.gameObject.CompareTag("Player"))
            {
                collisonSound.Play();   
                MicrophoneInput.instance.blowCharge -= 5f;
                Destroy(gameObject);
            }
        
    }


}
