using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float speed;
    public float airResist;
    public float jetMult = 1;
    public float height;
    public int money;
    [SerializeField] int maxLevel;
    public bool gameOn;
    [SerializeField] int EnemyLevel = 0;
    public List<EnemyInfo> enem;
    public List<CoinInfo> coins;
    public static event Action<EnemyInfo,CoinInfo,float> gameStart;
    public Transform scalingObj;
    [SerializeField]bool levelUp = false;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }
    void GameStart()
    {
        gameOn = true;
        gameStart?.Invoke(enem[EnemyLevel],coins[EnemyLevel],height);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn)
        {
            speed -= airResist * Time.deltaTime;
            height += speed * Time.deltaTime * jetMult;
            if (height > 1 && (int)height%20 == 0 && EnemyLevel < maxLevel && !levelUp)
            {
                levelUp = true;
                EnemyLevel++;
                Invoke("NextStage",2);
                gameStart?.Invoke(enem[EnemyLevel], coins[EnemyLevel], height);
            }
        }
    }
    void NextStage()
    {
        levelUp = false;
    }

    private void OnEnable()
    {
        CoinScript.AddMoney += AddCoin;
        MoveBehaviour.beginGame += GameStart;
    }
    private void OnDisable()
    {
        CoinScript.AddMoney -= AddCoin;
        MoveBehaviour.beginGame -= GameStart;
    }
    private void AddCoin(CoinInfo coin,int coinType)
    {
        money += coin.CoinList[coinType].ammount;
    }
}
