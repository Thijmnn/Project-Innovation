using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject shop;
    public static event Action startGame;
    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
        start.SetActive(true);
    }

    // Update is called once per frame
    public void OpenShop()
    {
        start.SetActive(false);
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        shop.SetActive(false);
        start.SetActive(true);
    }
    public void StartGame()
    {
        startGame?.Invoke();
        start.SetActive(false);
    }
}
