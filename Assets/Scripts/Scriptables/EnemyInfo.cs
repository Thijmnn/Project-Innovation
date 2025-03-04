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
    public GameObject EnemyType;
}
