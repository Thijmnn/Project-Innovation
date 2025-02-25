using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemInfo", menuName = "ScriptableObjects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public List<Info> EnemyList;
}
[Serializable]
public class Info
{
    public GameObject EnemyType;
    public int AmmountPerSpawn;
}
