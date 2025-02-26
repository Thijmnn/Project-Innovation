using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 10;
    [SerializeField] EnemyInfo enemy;
    List<float> prevPos = new List<float>();
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
        StopAllCoroutines();
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            int spawnChoice = Random.Range(0, enemy.EnemyList.Count);
            int randomSpawn = Random.Range(1, enemy.EnemyList[spawnChoice].AmmountPerSpawn);
            print(randomSpawn);
            for (int j = 1; j > 0; j++)
            {
                for (int i = 0; i < randomSpawn; i++)
                {
                    float check = enemy.EnemyList[0].EnemyType.transform.localScale.x / 2;
                    float randomX = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
                    for (int k = 0; k < prevPos.Count; k++)
                    {
                        if (prevPos[k] > randomX - check && prevPos[k] < randomX + check)
                        {
                            randomX += 1;
                        }
                    }
                    prevPos.Add(randomX);
                    Vector3 spawnPos = new Vector3(randomX, transform.position.y, transform.position.z);
                    print(spawnPos);
                    GameObject enem = Instantiate(enemy.EnemyList[0].EnemyType, spawnPos, transform.rotation);
                }
                prevPos.Clear();
                yield return new WaitForSeconds(timer);
            }
        }
    }
}
