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
    [SerializeField] GameObject story;
    public static event Action startGame;
    bool firstPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        if (firstPlay)
        {
            story.SetActive(true);
            
        }
        shop.SetActive(false);
        start.SetActive(false);
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

    public void closeComic()
    {
        start.SetActive(true);
        story.SetActive(false);
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
        if(firstPlay)
        StartTutorial();
        else
        {
            CloseTutorial();
        }
        start.SetActive(false);
    }
    public void StartTutorial()
    {
        tutorial.SetActive(true);
        firstPlay = false;
        Invoke("CloseTutorial", 3);
        
    }
    void CloseTutorial()
    {
        MicrophoneInput.instance.StartBlowing();
        tutorial.SetActive(false);
/*        startGame?.Invoke();*/
        GameUI();
    }
    void GameUI()
    {
        duringGame.SetActive(true);
    }
}
