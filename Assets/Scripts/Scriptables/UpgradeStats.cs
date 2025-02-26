using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeInfo", menuName = "ScriptableObjects/Player/UpgradeInfo")]

public class UpgradeStats : ScriptableObject
{
    [Header("UpgradeMultipliers")]
    public float speedMult;
    public float launchSpeed;
}
