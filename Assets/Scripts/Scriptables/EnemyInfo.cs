using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemInfo", menuName = "ScriptableObjects/Enemy/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public List<Info> EnemyList;
    public int minTimeBetweenSpawns;
    public int maxTimeBetweenSpawns;
    public int AmmountPerSpawn;
}
[Serializable]
public class Info
{
    public bool SpawnTop;
    public GameObject EnemyType;
}
