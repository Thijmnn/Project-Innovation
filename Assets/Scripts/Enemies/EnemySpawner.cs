using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using static Info;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action<Vector3, Info.spawnPosition, Info.warningTypes,bool> warning;
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
    bool leftSpawn;
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


                
                Vector3 spawnPos;
                int orient = 0;
                int rand = UnityEngine.Random.Range(1, 3);
                spawnChosen = spawnChoice;
                EnemSpawnPos = SetSpawnLocation(rand, orient);
                chosenOrent = orient;
            }
            prevPosEn.Clear();
            EnemyTimer = UnityEngine.Random.Range(enemy.minTimeBetweenSpawns, enemy.maxTimeBetweenSpawns);
            if (enemy.EnemyList[spawnChosen].warning)
            {
                warning?.Invoke(EnemSpawnPos, enemy.EnemyList[spawnChosen].pos, enemy.EnemyList[spawnChosen].warningType,leftSpawn);
            }
            if (!FirstSpawn) yield return new WaitForSeconds(EnemyTimer);
        }
    }

    private Vector3 SetSpawnLocation(int rand, int orien)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        float randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
        float randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 3, transform.position.y + transform.localScale.y / 3);

        orien = 1;
        switch (enemy.EnemyList[spawnChosen].pos)
        {
            case Info.spawnPosition.Up :
                randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
                spawnPos = new Vector3(randomX, transform.position.y + transform.localScale.y/2,0); break;

            case Info.spawnPosition.Down:
                orien = -1;
                randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
                spawnPos = new Vector3(randomX, transform.position.y - transform.localScale.y / 2, 0); break;

            case Info.spawnPosition.Left:
                randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 3, transform.position.y + transform.localScale.y / 3);
                spawnPos = new Vector3(transform.position.x -transform.localScale.x/2, randomY, 0); break;

            case Info.spawnPosition.Right:
                randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 3, transform.position.y + transform.localScale.y / 3);
                spawnPos = new Vector3(transform.position.x + transform.localScale.x / 2, randomY, 0); break;

            case Info.spawnPosition.Sides:
                randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y/3, transform.position.y + transform.localScale.y / 3);
                if (rand == 1) { randomX = transform.position.x - transform.localScale.x / 2; orien = -1; leftSpawn = true; }
                else if (rand == 2) { randomX = transform.position.x + transform.localScale.x / 2; orien = 1; leftSpawn = false; }
                spawnPos = new Vector3(randomX, randomY); break;

            case Info.spawnPosition.SidesTop:
                if (rand == 1) { randomX = transform.position.x - transform.localScale.x / 2; orien = -1; leftSpawn = true; } 
                else if (rand == 2) { randomX = transform.position.x + transform.localScale.x / 2; orien = 1; leftSpawn = false; } 
                randomY = UnityEngine.Random.Range(transform.position.y, transform.position.y + transform.localScale.y / 3);
                spawnPos = new Vector3(randomX, randomY, 0); break;

            case Info.spawnPosition.SidesBottom:
                if (rand == 1) { randomX = transform.position.x - transform.localScale.x / 2; orien = -1; leftSpawn = true; }
                else if (rand == 2) { randomX = transform.position.x + transform.localScale.x / 2; orien = 1; leftSpawn = false; }
                randomY = UnityEngine.Random.Range(transform.position.y, transform.position.y - transform.localScale.y / 3);
                spawnPos = new Vector3(randomX, randomY, 0); break;

            /*            case Info.spawnPosition.TopLeft: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.TopRight: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.BottomLeft: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.BottomRight: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.LeftTop: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.RightTop: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.LeftBottom: 
                            spawnPos = new Vector3(0, 0, 0); break;
                        case Info.spawnPosition.RightBottom: 
                            spawnPos = new Vector3(0, 0, 0); break;*/
            default:
         break;
        }
        

        return spawnPos;
    }
}
