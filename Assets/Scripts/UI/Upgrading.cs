using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrading : MonoBehaviour
{
    [SerializeField] UpgradeStats stats;
    public static event Action<UpgradeStats> upgrade;
    // Start is called before the first frame update
    void Upgrade()
    {
        upgrade?.Invoke(stats);
    }
}
