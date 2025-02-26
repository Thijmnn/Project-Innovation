using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float speed;
    public float jetMult = 1;
    public float height;
    public int money;
    bool gameOn;
    [SerializeField] int EnemyLevel = 0;
    public List<EnemyInfo> enem;
    public static event Action<EnemyInfo,float> gameStart;
    public Transform scalingObj;
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
        gameStart?.Invoke(enem[EnemyLevel],height);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn)
        {
            height += speed * Time.deltaTime * jetMult;
            if((int)height == 20 && EnemyLevel < 1)
            {
                EnemyLevel = 1;
                gameStart?.Invoke(enem[EnemyLevel], height);
            }
        }
    }
    private void OnEnable()
    {
        CoinScript.AddMoney += AddCoin;
        MoveBehaviour.jetMulti += SpeedUp;
    }
    private void OnDisable()
    {
        CoinScript.AddMoney -= AddCoin;
        MoveBehaviour.jetMulti -= SpeedUp;
    }
    private void AddCoin(CoinInfo coin)
    {
        money += coin.ammount;
    }
    private void SpeedUp(float mult)
    {
        jetMult = mult;
    }
}
