using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrading : MonoBehaviour
{
    [SerializeField] CostScript price;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] GameObject sprite;
    [SerializeField] UpgradeStats stats;
    public static event Action<UpgradeStats> upgrade;
    [SerializeField, HideInInspector] public int curUpgrade;
    int currentSprite = 0;

    // Start is called before the first frame update
    public void Upgrade()
    {
        if (price.cost <= GameManager.Instance.money)
        {
            curUpgrade += 1;
            if (currentSprite < sprites.Count - 1 && curUpgrade % 4 == 0)
            {
                currentSprite += 1;
                sprite.GetComponent<Image>().sprite = sprites[currentSprite];
            }
            upgrade?.Invoke(stats);
        }
    }
}
