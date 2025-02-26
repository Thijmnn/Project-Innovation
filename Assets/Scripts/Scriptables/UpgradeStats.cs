using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeInfo", menuName = "ScriptableObjects/Player/UpgradeInfo")]

public class UpgradeStats : ScriptableObject
{
    [Header("UpgradeMultipliers")]
    public float speedMult;
    public float launchSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
