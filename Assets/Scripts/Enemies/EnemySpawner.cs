using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 10;
    EnemyInfo enemy;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, 0.1f);
    }

    private void OnEnable()
    {
        GameManager.gameStart += GameStart;
    }
    private void DisEnable()
    {
        GameManager.gameStart -= GameStart;
    }


    private void GameStart(EnemyInfo e, float height)
    {
        enemy = e;
        Invoke("Spawn", 0);
    }
    void Spawn()
    {
        int spawnChoice = Random.Range(0, enemy.EnemyList.Count);
        int randomSpawn = Random.Range(1, enemy.EnemyList[spawnChoice].AmmountPerSpawn);
        for (int i = 0; i < randomSpawn; i++)
        {
            float randomX = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
            Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
            GameObject enem = Instantiate(enemy.EnemyList[0].EnemyType, spawnPos, transform.rotation);
        }

        Invoke("Spawn", timer);
    }
}
