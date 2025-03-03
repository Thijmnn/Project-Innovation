using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action<bool, bool> moving;
    float EnemyTimer = 10;
    float CoinTimer = 5;
    [SerializeField] EnemyInfo enemy;
    CoinInfo coins;
    List<float> prevPosEn = new List<float>();
    List<float> prevPosCo = new List<float>();
    bool FirstSpawn;
    Vector3 EnemSpawnPos;
    int spawnChosen;
    int chosenOrent;
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


    private void GameStart(EnemyInfo e, CoinInfo coin, float height)
    {
        enemy = e;
        coins = coin;
        FirstSpawn = true;
        StopAllCoroutines();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            int spawnChoice = UnityEngine.Random.Range(0, coins.CoinList.Count);
            int randomSpawn = UnityEngine.Random.Range(1, coins.CoinList[spawnChoice].ammountPerSpawn);
            print(randomSpawn);
            for (int j = 1; j > 0; j++)
            {
                for (int i = 0; i < randomSpawn; i++)
                {
                    float check = coins.CoinList[spawnChoice].CoinType.transform.localScale.x / 2;
                    float randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
                    for (int k = 0; k < prevPosCo.Count; k++)
                    {
                        if (prevPosCo[k] > randomX - check && prevPosCo[k] < randomX + check)
                        {
                            randomX += 1;
                        }
                    }
                    prevPosCo.Add(randomX);
                    Vector3 spawnPos = new Vector3(randomX, transform.position.y + transform.localScale.y / 2, transform.position.z);
                    print(spawnPos);
                    GameObject coin = Instantiate(coins.CoinList[spawnChoice].CoinType, spawnPos, transform.rotation);
                    coin.GetComponent<CoinScript>().coinType = spawnChoice;
                    coin.GetComponent<CoinScript>().coin = coins;
                }
                prevPosCo.Clear();
                CoinTimer = UnityEngine.Random.Range(coins.minTimeBetweenSpawns, coins.maxTimeBetweenSpawns);

                yield return new WaitForSeconds(CoinTimer);
            }
        }
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int randomSpawn = UnityEngine.Random.Range(1, enemy.AmmountPerSpawn);
            print(randomSpawn);
            for (int i = 0; i < randomSpawn; i++)
            {
                if (!FirstSpawn)
                {
                    GameObject enem = Instantiate(enemy.EnemyList[spawnChosen].EnemyType, EnemSpawnPos, transform.rotation);
                    enem.GetComponent<EnemyMoveBehaviour>().orientation = chosenOrent;
                }
                int spawnChoice = UnityEngine.Random.Range(0, enemy.EnemyList.Count);
                float check = enemy.EnemyList[spawnChoice].EnemyType.transform.localScale.x / 2;
                float randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
                float randomY = UnityEngine.Random.Range(transform.position.y + transform.localScale.y / 3, transform.position.y + transform.localScale.y / 3);
                for (int k = 0; k < prevPosEn.Count; k++)
                {
                    if (prevPosEn[k] > randomX - check && prevPosEn[k] < randomX + check)
                    {
                        randomX += 1;
                    }
                }
                prevPosEn.Add(randomX);
                int orient;
                Vector3 spawnPos;
                if (enemy.EnemyList[spawnChoice].SpawnTop)
                {
                    spawnPos = new Vector3(randomX, transform.position.y + transform.localScale.y / 2, transform.position.z);
                    orient = 1;
                }
                else
                {
                    int rand = UnityEngine.Random.Range(1, 3);
                    if (rand == 1)
                    {
                        spawnPos = new Vector3(transform.position.x + transform.localScale.x / 2, randomY, transform.position.z);
                        orient = 1;
                    }
                    else
                    {
                        spawnPos = new Vector3(transform.position.x - transform.localScale.x / 2, randomY, transform.position.z);
                        orient = -1;
                    }

                }
                spawnChosen = spawnChoice;
                EnemSpawnPos = spawnPos;
                chosenOrent = orient;
            }
            prevPosEn.Clear();
            EnemyTimer = UnityEngine.Random.Range(enemy.minTimeBetweenSpawns, enemy.maxTimeBetweenSpawns);
            if(!FirstSpawn) yield return new WaitForSeconds(EnemyTimer);
        }
    }
}
