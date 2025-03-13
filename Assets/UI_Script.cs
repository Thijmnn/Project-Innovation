using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject duringGame;
    public static event Action startGame;
    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
        start.SetActive(true);
        tutorial.SetActive(false);
        duringGame.SetActive(false);
    }


    private void OnEnable()
    {
        GameManager.gameRestart += Start;
    }
    private void DisEnable()
    {
        GameManager.gameRestart += Start;
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
        StartTutorial();
        start.SetActive(false);
    }
    public void StartTutorial()
    {
        tutorial.SetActive(true);
        Invoke("CloseTutorial", 3);
    }
    void CloseTutorial()
    {
        tutorial.SetActive(false);
        startGame?.Invoke();
        GameUI();
    }
    void GameUI()
    {
        duringGame.SetActive(true);
    }
}
