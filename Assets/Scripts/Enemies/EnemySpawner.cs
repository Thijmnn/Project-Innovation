using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

using UnityEngine;
using UnityEngine.UIElements;
using static Info;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action<Vector3, Info.spawnPosition, Info.warningTypes, bool> warning;
    [SerializeField] float EnemyTimer = 10;
    [SerializeField] EnemyInfo enemy;
    Vector3 EnemSpawnPos;
    int chosenOrent;
    bool leftSpawn;
    bool firstSpawn = true;
    int spawnChosen;
    int prevSpawnChose;
    List<float> prevSpawnPos = new List<float>();

    float CoinTimer = 5;
    CoinInfo coins;
    List<float> prevPosCo = new List<float>();

    List<Vector3> spawnPosEnemy = new List<Vector3>();
    List<GameObject> spawnTypeWarning = new List<GameObject>();
    List<float> spawnTimer = new List<float>();
    List<int> warningSpawnOrientation = new List<int>();

    bool skipSpawn;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, -0.5f);
    }

    private void OnEnable()
    {
        GameManager.gameStart += GameStart;
    }
    private void OnDisable()
    {
        GameManager.gameStart -= GameStart;
    }


    private void GameStart(EnemyInfo e, CoinInfo coin, float height)
    {
        enemy = e;
        e.Start();
        coins = coin;
        firstSpawn = true;
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
            if (firstSpawn)
            {
                firstSpawn = false;
                yield return new WaitForSeconds(2);
            }
            int randomSpawn = UnityEngine.Random.Range(enemy.MinAmmountPerSpawn, enemy.MaxAmmountPerSpawn);
            for (int i = 0; i < randomSpawn; i++)
            {
                int spawnChoice = UnityEngine.Random.Range(0, 100);
                for (int j = 0; j < enemy.EnemyList.Count; j++)
                {
                    if (spawnChoice >= enemy.spawnChanceList[j] && spawnChoice < (enemy.spawnChanceList[j] + enemy.spawnChanceList[j + 1]))
                    {
                        spawnChoice = j;
                        break;
                    }
                    else if (j == enemy.EnemyList.Count - 1) spawnChoice = j;
                }
                float check = enemy.EnemyList[spawnChoice].EnemyType.transform.localScale.x / 2;

                int orient = 0;
                int rand = UnityEngine.Random.Range(1, 3);
                spawnChosen = spawnChoice;
                EnemSpawnPos = SetSpawnLocation(rand, orient, i);

                if (enemy.EnemyList[spawnChosen].warning)
                {
                    warning?.Invoke(EnemSpawnPos, enemy.EnemyList[spawnChosen].pos, enemy.EnemyList[spawnChosen].warningType, leftSpawn);
                    spawnTimer.Add(0);
                    spawnTypeWarning.Add(enemy.EnemyList[spawnChosen].EnemyType);
                    spawnPosEnemy.Add(EnemSpawnPos);
                    warningSpawnOrientation.Add(chosenOrent);
                    continue;
                }

                GameObject enem = Instantiate(enemy.EnemyList[spawnChosen].EnemyType, EnemSpawnPos, transform.rotation);
                enem.GetComponent<EnemyMoveBehaviour>().orientation = chosenOrent;
                prevSpawnChose = spawnChosen;

            }
            prevSpawnPos.Clear();
            EnemyTimer = UnityEngine.Random.Range(enemy.minTimeBetweenSpawns, enemy.maxTimeBetweenSpawns);
            yield return new WaitForSeconds(EnemyTimer);
        }
    }
    void Update()
    {
        if (spawnTimer.Count != 0)
        {
            for (int i = 0; i < spawnTimer.Count; i++)
            {
                spawnTimer[i] += Time.deltaTime;
            }
        }
        if (spawnTimer.Count != 0)
        {
            if (spawnTimer[0] > 2.0f)
            {
                GameObject warnEn = Instantiate(spawnTypeWarning[0], spawnPosEnemy[0], transform.rotation);
                warnEn.GetComponent<EnemyMoveBehaviour>().orientation = warningSpawnOrientation[0];
                warningSpawnOrientation.RemoveAt(0);
                spawnTypeWarning.RemoveAt(0);
                spawnPosEnemy.RemoveAt(0);
                spawnTimer.RemoveAt(0);
            }
        }
    }

    private Vector3 SetSpawnLocation(int rand, int orien, int spawnNumb)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        float randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
        float randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 3, transform.position.y + transform.localScale.y / 3);

        orien = 1;
        switch (enemy.EnemyList[spawnChosen].pos)
        {
            case Info.spawnPosition.Up:
                randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    print(spawnNumb);
                    if (randomX >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x && randomX <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x)
                    {
                        if (randomX > transform.position.x) { randomX += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x; i = 0; }
                        else { randomX -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x; i = 0; }
                    }
                }
                spawnPos = new Vector3(randomX, transform.position.y + transform.localScale.y / 2, 0);
                prevSpawnPos.Add(randomX);
                break;

            case Info.spawnPosition.Down:
                orien = -1;
                randomX = UnityEngine.Random.Range(transform.position.x - transform.localScale.x / 3, transform.position.x + transform.localScale.x / 3);
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    if (randomX >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x && randomX <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x)
                    {
                        if (randomX > transform.position.x) { randomX += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x; i = 0; }
                        else { randomX -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.x; i = 0; }
                    }
                }
                spawnPos = new Vector3(randomX, transform.position.y - transform.localScale.y / 2, 0);
                prevSpawnPos.Add(randomX);
                break;

            case Info.spawnPosition.Left:
                randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 4, transform.position.y + transform.localScale.y / 4);
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    if (randomY >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y && randomY <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y)
                    {
                        if (randomY > transform.position.y) { randomY += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                        else { randomY -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                    }
                }
                spawnPos = new Vector3(transform.position.x - transform.localScale.x / 2, randomY, 0);
                prevSpawnPos.Add(randomY);
                break;

            case Info.spawnPosition.Right:
                randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 4, transform.position.y + transform.localScale.y / 4);
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    if (randomY >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y && randomY <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y)
                    {
                        if (randomY > transform.position.y) { randomY += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                        else { randomY -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                    }
                }
                spawnPos = new Vector3(transform.position.x + transform.localScale.x / 2, randomY, 0);
                prevSpawnPos.Add(randomY);
                break;

            case Info.spawnPosition.Sides:
                randomY = UnityEngine.Random.Range(transform.position.y - transform.localScale.y / 4, transform.position.y + transform.localScale.y / 4);
                if (rand == 1) { randomX = transform.position.x - transform.localScale.x / 2; orien = -1; leftSpawn = true; }
                else if (rand == 2) { randomX = transform.position.x + transform.localScale.x / 2; orien = 1; leftSpawn = false; }
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    if (randomY >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y && randomY <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y)
                    {
                        if (randomY > transform.position.y) { randomY += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                        else { randomY -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                    }
                }
                spawnPos = new Vector3(randomX, randomY);
                prevSpawnPos.Add(randomY);
                break;

            case Info.spawnPosition.SidesTop:
                if (rand == 1) { randomX = transform.position.x - transform.localScale.x / 2; orien = -1; leftSpawn = true; }
                else if (rand == 2) { randomX = transform.position.x + transform.localScale.x / 2; orien = 1; leftSpawn = false; }
                randomY = UnityEngine.Random.Range(transform.position.y, transform.position.y + transform.localScale.y / 4);
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    if (randomY >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y && randomY <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y)
                    {
                        if (randomY > transform.position.y) { randomY += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                        else { randomY -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                    }
                }
                spawnPos = new Vector3(randomX, randomY, 0);
                prevSpawnPos.Add(randomY);
                break;

            case Info.spawnPosition.SidesBottom:
                if (rand == 1) { randomX = transform.position.x - transform.localScale.x / 2; orien = -1; leftSpawn = true; }
                else if (rand == 2) { randomX = transform.position.x + transform.localScale.x / 2; orien = 1; leftSpawn = false; }
                randomY = UnityEngine.Random.Range(transform.position.y, transform.position.y - transform.localScale.y / 4);
                for (int i = 0; i < prevSpawnPos.Count; i++)
                {
                    if (randomY >= prevSpawnPos[i] - enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y && randomY <= prevSpawnPos[i] + enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y)
                    {
                        if (randomY > transform.position.y) { randomY += enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                        else { randomY -= enemy.EnemyList[prevSpawnChose].EnemyType.transform.localScale.y * i; i = 0; }
                    }
                }
                spawnPos = new Vector3(randomX, randomY, 0);
                prevSpawnPos.Add(randomY);
                break;

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
        /*for (int i = 0;i < prevSpawnPos.Count; i++)
        {
            if(s)
        }*/
        chosenOrent = orien;

        return spawnPos;
    }
}
