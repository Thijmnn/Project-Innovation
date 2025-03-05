using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemInfo", menuName = "ScriptableObjects/Enemy/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public List<Info> EnemyList;
    public int minTimeBetweenSpawns;
    public int maxTimeBetweenSpawns;
    public int MinAmmountPerSpawn;
    public int MaxAmmountPerSpawn;

    [HideInInspector]
    public List<int> spawnChanceList;
    public void Start()
    {
        spawnChanceList.Add(0);
        for (int i = 0; i < EnemyList.Count; i++)
        {
            spawnChanceList.Add(EnemyList[i].spawnChanceOutOf100);
        }
    }
}
[Serializable]
public class Info
{
    public GameObject EnemyType;
    public int spawnChanceOutOf100;
public enum spawnPosition
{
    Up,
    Down,
    Left,
    Right,
    Sides,
    SidesTop,
    SidesBottom,
/*    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    LeftTop,
    RightTop,
    LeftBottom,
    RightBottom,*/
};
    public spawnPosition pos;
    public bool warning;
    public enum warningTypes
    {
        small,
        big
    };
    public warningTypes warningType;
}
