using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinInfo", menuName = "ScriptableObjects/Pickups/CoinInfo")]
public class CoinInfo : ScriptableObject
{
    public List<CInfo> CoinList;
    public int minTimeBetweenSpawns;
    public int maxTimeBetweenSpawns;
}
[Serializable]
public class CInfo
{
    public int ammountPerSpawn;
    public GameObject CoinType;
    public int ammount;
}
