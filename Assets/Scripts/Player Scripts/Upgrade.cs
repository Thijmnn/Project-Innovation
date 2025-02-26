using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    MoveBehaviour movement;
    ShootingBehaviour shooting;
    private void OnEnable()
    {
        Upgrading.upgrade += UpgradingStats;
    }
    private void OnDisable()
    {
        Upgrading.upgrade -= UpgradingStats;
    }
    public void SetRefrences(MoveBehaviour move, ShootingBehaviour shoot = null)
    {
        movement = move;
        shooting = shoot;
    }
    void UpgradingStats(UpgradeStats up)
    {
        movement.speed *= up.speedMult;
    }

}
