using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int levelUpInterval;
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

            if(height > levelUpInterval * (EnemyLevel+1) && EnemyLevel < maxLevel-1)
            {
                EnemyLevel++;
                print(EnemyLevel);
                gameStart?.Invoke(enem[EnemyLevel], coins[EnemyLevel], height);
            }
 
        }
    }

    private void OnEnable()
    {
        CoinScript.AddMoney += AddCoin;
        MoveBehaviour.beginGame += GameStart;
        UI_Script.startGame += GameStart;
    }
    private void OnDisable()
    {
        CoinScript.AddMoney -= AddCoin;
        MoveBehaviour.beginGame -= GameStart;
        UI_Script.startGame -= GameStart;
    }
    private void AddCoin(CoinInfo coin,int coinType)
    {
        money += coin.CoinList[coinType].ammount;
    }
}
